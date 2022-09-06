using Hangfire.Console;
using Hangfire.Server;
using System;
using Microsoft.Extensions.Logging;
using Hangfire.Console.Progress;
using System.Collections.Generic;
using System.Linq;
using Serilog.Context;
using Serilog.Core.Enrichers;

namespace eveDirect.Services.Jobs.Core
{
    /// <summary>
    /// Базовый класс для выполнения background задач
    /// </summary>
    public class JobBase : IDisposable
    {
        /// <summary>
        /// Ограничение на максимальное количество элементов на запрос
        /// </summary>
        protected int Max_Items_To_Request { get; set; }
        protected PerformContext _performContext { get; set; }
        protected IProgressBar bar { get; set; }
        protected Random random { get; set; }

        protected readonly ILogger _logger;
        protected string jobName { get; }
        protected JobResult _jobResult { get; set; }

        public JobBase(ILogger logger) {
            _logger = logger;
            random = new Random();
            
            // для лога
            jobName = this.GetType().Name;
            //LogContext.PushProperty("jobName", jobName);

            _jobResult = new JobResult();
        }

        #region Properties
        /// <summary>
        /// Название задания
        /// </summary>
        protected string Job_Name { get; set; }
        /// <summary>
        /// Последний шаг в ходе выполнения задания
        /// </summary>
        protected string Last_Step { get; set; }
        /// <summary>
        /// Время следующего запуска по расписанию
        /// </summary>
        protected DateTime Next_Fire { get; set; }
        /// <summary>
        /// Выполнение задачи параллельно
        /// </summary>
        protected bool Execute_Parallel { get; set; }
        #endregion

        /// <summary>
        /// Выполнение задания
        /// </summary>
        public virtual void Execute() {
            
        }
        /// <summary>
        /// Виртуальный метод, который переопределяется дочерними job-классами
        /// </summary>
        public virtual void TaskJob(PerformContext performContext = null) {
            _performContext = performContext;

            Execute();

            ToResult();
        }

        public virtual void ToResult() {
            // Генерация строки
            string line = $"{_jobResult.Value}";
            foreach (var item in _jobResult.subValues)
                line += $" {item.Name}:{item.Value}";

            // Отправка в Hangfire Console
            ToConsole("Result: " + line, false);

            // Отправка в лог
            if(_logger != null)
            {
                var propertires = _jobResult.subValues.Select(p => new PropertyEnricher(p.Name, p.Value, false)).ToList();
                propertires.Add(new PropertyEnricher(_jobResult.Name, _jobResult.Value, false));

                using (LogContext.Push(propertires.ToArray()))
                {
                    _logger.LogInformation(line);
                }
            }
        }

        protected void ToConsole(string message, bool toLog=true)
        {
            _performContext?.WriteLine(message);

            if (toLog)
            {
                LogInfo(message);
            }
        }

        protected IEnumerable<T> AttachProgressBarToList<T>(List<T> list)
        {
            if(_performContext != null)
            {
                bar = _performContext.WriteProgressBar();
                return list.WithProgress(bar);
            }

            return list.OfType<T>();
        }

        protected IEnumerable<T> AttachProgressBarToList<T>(IEnumerable<T> list)
        {
            if (_performContext != null)
            {
                bar = _performContext.WriteProgressBar();
                return list.WithProgress(bar, list.Count());
            }

            return list;
        }

        protected int last_value = 0, last_index = 0;
        protected void SetValue(int index, int count)
        {
            var step = index * 100 / count;
            if (last_value < step)
            {
                bar?.SetValue(step);
                last_value = step;
            }
        }

        protected void LogInfo(string msg)
        {
            _logger?.LogInformation(msg);
        }
        protected void LogError(string msg)
        {
            _logger?.LogError(msg);
        }

        public virtual void Dispose()
        {

        }
    }

    public class JobResult
    {
        public string Name { get; set; } = "Result";
        public int Value { get; set; }
        public bool Success { get; set; }

        public List<Item> subValues { get; set; } = new List<Item>();

        public class Item
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
    }
}
