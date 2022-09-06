using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using eveDirect.Clients.Web.Models;
using eveDirect.Clients.Web.Hubs;

namespace eveDirect.Clients.Web.Services
{
    /// <summary>
    /// Сервис последних действий в публичных сервисах
    /// </summary>
    public class LastActionsService: ILastActionsService
    {
        private readonly Timer _timer;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(700);
        private readonly object _updateStockPricesLock = new object();
        Random random = new Random();
        int length = Enum.GetNames(typeof(ELastActionType)).Length;
        int limit = 50;
        long last_id = 0;

        public LastActionsService(IHubContext<LastActionsHub> hubContext, IWebHostEnvironment environment)
        {
            lastActions = new Queue<LastActionModel>();
            _hubContext = hubContext;
            //if (environment.IsDevelopment())
            //{
            //    GenerateOnStartup();
            //    _timer = new Timer(AddRandom, null, _updateInterval, _updateInterval);
            //}

            last_id = DateTime.UtcNow.Ticks;
        }

        private void AddRandom(object state)
        {
            lock (_updateStockPricesLock)
            {
                Add(GenerateNew());
            }
        }

        LastActionModel GenerateNew()
        {
            return new LastActionModel()
            {
                type = (ELastActionType)random.Next(0, length),
                item_id = random.Next(100000, 2000000000)
            };
        }

        void GenerateOnStartup()
        {
            int i = 0;
            do
            {
                lastActions.Enqueue(GenerateNew());
                i++;
            }
            while (i < 50);
        }

        /// <summary>
        /// Последние действия
        /// </summary>
        readonly Queue<LastActionModel> lastActions;
        IHubContext<LastActionsHub> _hubContext { get; set; }

        /// <summary>
        /// Добавление элемента
        /// </summary>
        public void Add(LastActionModel item)
        {
            item.i = last_id++;

            lastActions.Enqueue(item);
            updateToClients(item);
            if(lastActions.Count > limit)
                lastActions.Dequeue();
        }

        /// <summary>
        /// Получение стека
        /// </summary>
        public IEnumerable<LastActionModel> All()
        {
            return lastActions.AsEnumerable();
        }

        void updateToClients(LastActionModel item)
        {
            _hubContext.Clients.All.SendAsync("newInsert", item);
        }
    }
}
