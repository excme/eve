using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.Heartbeat;
using System;
using Hangfire.Heartbeat.Server;
using Hangfire.Dashboard;
using Hangfire.Console;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using eveDirect.Shared.WebHost;
using eveDirect.Shared.Helper;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.ReadWrite;
using StackExchange.Redis;
using Hangfire.MemoryStorage;

namespace eveDirect.Services.Jobs.Web
{
    public class Startup
    {
        IConfiguration configuration { get; }
        IWebHostEnvironment environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Подключение к БД
            string connString = environment.IsProduction() 
                ? configuration["ConnectionStrings:PublicDb"] 
                : configuration["ConnectionStrings:PublicDb-Test"];
            services.AddDbOptions<PublicContext>(connString);

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // MVC Hangfire
            services.AddMvc(option => option.EnableEndpointRouting = false);

            // RabbitMQ
            services.ConnectToRabbitMQ(configuration, Program.AppName, environment);

            // Подключение репозитория
            services.AddTransient<IReadWrite, ReadWriteRepo>();

            // Cache
            //services.AddSingleton<ICustomDistibutedCache, CustomDistibutedCache>();
            
            // Filters Hangfire
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0, LogEvents = true, OnAttemptsExceeded = AttemptsExceededAction.Fail });
            GlobalJobFilters.Filters.Add(new SkipConcurrentExecutionAttribute(10));

            //var redisConnection = new ConfigurationOptions() { 
            //    EndPoints = {
            //        { configuration["HangFire:RedisAddress"], 6379}
            //    },
            //    Password = configuration["HangFire:RedisPassword"]
            //};

            services.AddHangfire((provider, _configuration) => _configuration
                //.UseSimpleAssemblyNameTypeSerializer()
                .UseConsole()
                .UseSerilogLogProvider()
                //.UseRecommendedSerializerSettings()
                .UseHeartbeatPage(checkInterval: TimeSpan.FromSeconds(5))
                // Redis
                //.UseRedisStorage(ConnectionMultiplexer.Connect(redisConnection), new Hangfire.Redis.RedisStorageOptions()
                //{
                //    // Максимальное время выполнения экзепляра задачи. После уходит на перезапуск
                //    InvisibilityTimeout = TimeSpan.FromHours(4),
                //    ExpiryCheckInterval = TimeSpan.FromMinutes(3),
                //    //FetchTimeout = TimeSpan.FromMinutes(3),
                //    //UseTransactions = false,
                //    // It is highly recommended to set the Prefix property, to avoid overlap with other projects that targets the same Redis store!
                //    //Prefix = "{hangfire}:"// + $"{Program.AppName}" + ":"
                //    Prefix = "{eve"+$"-{(environment.IsProduction() ? "prod" : "dev")}" + "}:"
                //})
                .UseMemoryStorage(new MemoryStorageOptions()
                {
                    CountersAggregateInterval = TimeSpan.FromMinutes(1),
                    JobExpirationCheckInterval = TimeSpan.FromMinutes(3),
                })
                //.UsePostgreSqlStorage(Configuration["HangFire:Postgres"], new PostgreSqlStorageOptions()
                //{
                //    InvisibilityTimeout = TimeSpan.FromHours(4)
                //})
                );

            services.AddOptions();

            // Jobs Fire and Forget
            //services.AddTransient<RecalcCharacterMigrations>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app/*, IBackgroundJobClient backgroundJobs,*//* IWebHostEnvironment env*/)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            #region Hangfire
            //JobStorage.Current.GetConnection().RemoveServer("eveDirect");

            var _temp = configuration["Queues"];
            var queues = _temp != null
                ? JsonSerializer.Deserialize<List<string>>(_temp)
                : new List<string>();

            var options = new BackgroundJobServerOptions
            {
                ServerName = Program.AppName, 
                WorkerCount = 40,
                Queues = queues.ToArray(), 
                
                CancellationCheckInterval = TimeSpan.FromSeconds(60),

                // Ожидание пере запуском
                SchedulePollingInterval = TimeSpan.FromSeconds(5),
                
                HeartbeatInterval = TimeSpan.FromSeconds(30 * 2),
                ServerTimeout = TimeSpan.FromMinutes(5 * 2),
                ServerCheckInterval = TimeSpan.FromMinutes(5 * 2)
            };

            app.UseHangfireServer(options, additionalProcesses: new[] { new ProcessMonitor(checkInterval: TimeSpan.FromSeconds(3)) });

            app.UseHangfireDashboard("/uawj8w39d83h8", new DashboardOptions()
            {
                IgnoreAntiforgeryToken = true,
                Authorization = new[] { new HangfireAuthorizationFilter(environment.IsDevelopment()) }
            });
            #endregion

            // Загрузка расписаний
            List<EJobsCategories> l = queues.Select(a => EnumGeneric.ToEnum<EJobsCategories>(a)).ToList();
            JobsList.Load(l);
        }
    }

    // Access by Сookie in Hangfire Dashboard
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        bool isDevelopment { get; set; }
        public HangfireAuthorizationFilter(bool development)
        {
            isDevelopment = development;
        }
        public bool Authorize(DashboardContext context)
        {
            try
            {
                //if (!isDevelopment) { 
                //    var httpContext = context.GetHttpContext();
                //    var userRole = httpContext.Request.Cookies["AuthToken"];
                //    return userRole == "j9as8dj32nobas9d8huihAJ9s9dja9j3mdaoisdhn_Asdl_a!!!la0sdj";
                //}

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
