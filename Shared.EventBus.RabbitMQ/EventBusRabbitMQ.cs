using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Shared.EventBus.Events;
using eveDirect.Shared.EventBus.Extensions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace eveDirect.Shared.EventBus.EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        string BROKER_NAME { get; }

        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _retryCount;

        private IModel _consumerChannel;
        private string _queueName;

        private bool subscribesCompleted { get; set; }
        private Dictionary<KeyValuePair<DateTime, string>, int> statisticUsage;

        public EventBusRabbitMQ(
            IRabbitMQPersistentConnection persistentConnection, 
            ILogger<EventBusRabbitMQ> logger,
            IServiceProvider serviceProvider,
            IEventBusSubscriptionsManager subsManager,
            string brokerName,
            string queueName = null, 
            int retryCount = 5)
        {
            BROKER_NAME = brokerName; 
            
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _queueName = queueName;
            _consumerChannel = CreateConsumerChannel();
            _serviceProvider = serviceProvider;
            _retryCount = retryCount;

            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;

            statisticUsage = new Dictionary<KeyValuePair<DateTime, string>, int>();
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName,
                    exchange: BROKER_NAME,
                    routingKey: eventName);

                if (_subsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, $"Не смог опубликовать событие: {@event.Id} after {time.TotalSeconds:n1}s ({ex.Message})");
                });

            var eventName = @event.GetType().Name;

            _logger.LogTrace($"Создание RabbitMQ channel для публикации сообщения: {@event.Id} ({eventName})");

            using (var channel = _persistentConnection.CreateModel())
            {
                _logger.LogTrace($"Декларирование RabbitMQ точки доступа для публикации сообщения: {@event.Id}");

                channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

                var message = JsonSerializer.Serialize(@event, @event.GetType());
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent

                    _logger.LogTrace($"Публикация события в RabbitMQ: {@event.Id}");

                    channel.BasicPublish(
                        exchange: BROKER_NAME,
                        routingKey: eventName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                });

                // Запись Postgres
                //_rabbitRepo.RabbitMQEvent_Add(new EveDirectRabbitEvent()
                //{
                //    id = @event.Id,
                //    type = eventName,
                //    data = @event
                //});
            }
        }

        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation($"Подписка на  динамическое событие {eventName} слушателем {typeof(TH).GetGenericTypeName()}");

            DoInternalSubscription(eventName);
            _subsManager.AddDynamicSubscription<TH>(eventName);
            StartBasicConsume();
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);

            _logger.LogInformation($"Подписка на событие {eventName} слушателем {typeof(TH).GetGenericTypeName()}");

            _subsManager.AddSubscription<T, TH>();
            StartBasicConsume();
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName,
                        exchange: BROKER_NAME,
                        routingKey: eventName);
                }
            }
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            _logger.LogInformation($"Отписка от события {eventName}");

            _subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }

            _subsManager.Clear();
        }

        private void StartBasicConsume()
        {
            _logger.LogTrace("Начало RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                // Для мульти подписчиков на один queue
                _consumerChannel.BasicQos(
                    prefetchSize: 0,
                    prefetchCount: 1,
                    global: false
                );

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume() не может быть вызван при _consumerChannel == null");
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake исключение запрошено: \"{message}\"");
                }

                await ProcessEvent(eventName, message);
                
                // Even on exception we take the message off the queue.
                // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
                // For more information see: https://www.rabbitmq.com/dlx.html
                _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: true);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"----- Ошибка при Обработке события \"{message}\"");
                _consumerChannel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: true);
            }
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _logger.LogTrace("Создание RabbitMQ слушателя channel");

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME,
                                    type: "direct");

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Пересоздание RabbitMQ слушателя channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace($"Обработка RabbitMQ события: {eventName}");

            while (!subscribesCompleted)
            {
                Thread.Sleep(10 * 1000);
            }

            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    var handler = _serviceProvider.GetService(subscription.HandlerType);
                    if (handler == null) 
                        continue;

                    Type eventType = _subsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType);

                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await Task.Yield();

                    // Проверяем, есть ли запись о событии в 
                    //Guid eventId = (integrationEvent as IntegrationEvent).Id;
                    //bool can = _rabbitRepo.RabbitMQEvent_Exists(eventId);

                    //if (can)
                    //{
                        Statistic_Add(eventType.Name);

                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });

                        //_rabbitRepo.RabbitMQEvent_Remove(eventId);

                    //}
                }
            }
            else
            {
                _logger.LogWarning($"Нет подписки для RabbitMQ события: {eventName}");
            }
        }

        public void SubscribesCompleted()
        {
            subscribesCompleted = true;
        }

        public void Statistic_Add(string eventName)
        {
            var key = new KeyValuePair<DateTime, string>(DateTime.UtcNow.Date, eventName);
            var exist_item = statisticUsage.ContainsKey(key);
            if (exist_item)
            {
                statisticUsage[key] += 1;

                // Проверка на удаление старых записей
                var keys = statisticUsage.Keys.ToList();
                var keys_dates = keys.GroupBy(g => g.Key).Select(x => x.Key).ToList();
                if(keys_dates.Count > 5)
                {
                    var date_toRemove = keys_dates.Min();
                    keys.ForEach(x =>
                    {
                        if (x.Key == date_toRemove)
                            statisticUsage.Remove(x);
                    });
                }

            }
            else
            {
                statisticUsage.Add(key, 1);
            }
        }

        public List<string> Statistic_Read()
        {
            List<string> strings = new List<string>();
            foreach (var item in statisticUsage)
            {
                strings.Add($"{item.Key.Key.Date}|{item.Key.Value}|{item.Value}");
            }

            return strings;
        }
    }
}