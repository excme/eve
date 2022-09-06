using Microsoft.EntityFrameworkCore;
using System;
using Xunit.Abstractions;
using Moq;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Options;
using eveDirect.Shared.ConfigurationHelper;
using eveDirect.Caching;
using eveDirect.Shared.EsiConnector;
using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.EsiConnector.Models.SSO;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Shared.GeneralTest
{
    public class UnitTestCore
    {
        protected ITestOutputHelper _output { get; set; }
        // Коннектор
        protected string clientId { get; set; }
        protected string secretKey { get; set; }

        protected IEventBus _eventBus { get; set; }
        protected ICustomDistibutedCache _cache { get; set; }

        // Repos
        //protected IRepoPublicWarsKillmails _repoPublicWarsKillmails { get; set; }
        protected IReadWrite _repoPublicCommon { get; set; }
        protected IReadOnly _repoReadOnly { get; set; }

        // DbContextOptions
        protected DbContextOptions<PublicContext> _publicContextOptions { get; set; }

        protected EsiClient connector { get; set; }
        // Markus Dallocort
        protected int character_id = 90522832;

        public UnitTestCore(ITestOutputHelper output)
        {
            _output = output;// ?? throw new ArgumentNullException(nameof(output));

            _eventBus = new Mock<IEventBus>().Object;
            _cache = new Mock<ICustomDistibutedCache>().Object;

            // Коннектор
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettingsGeneral.json", optional: true, reloadOnChange: true);

            var conf = confBuilder.Build();

            clientId = conf.GetValue<string>("SSO:ClientId");
            secretKey = conf.GetValue<string>("SSO:Secret");

            // Соединения с БД и миграция
            update_DbConnOprions("PublicDb-UnitTest");

            // Миграции БД
            using var dbContext = new PublicContext(_publicContextOptions);
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
                dbContext.Database.Migrate();

            // Коннектор
            IOptions<EsiConfig> config = Options.Create(new EsiConfig()
            {
                EsiUrl = "https://esi.evetech.net/",
                DataSource = DataSource.tranquility,
                ClientId = conf.GetValue<string>("Sso:clientId"),
                SecretKey = conf.GetValue<string>("Sso:secret"),
                CallbackUrl = ""
            });
            connector = new EsiClient(config);
        }

        protected void update_DbConnOprions(string key)
        {
            _publicContextOptions = LoadContextOptions<PublicContext>(key);
            _repoPublicCommon = new ReadWriteRepo(_eventBus, _publicContextOptions);
            _repoReadOnly = new ReadOnly(_publicContextOptions, _cache);
        }

        protected void Esi_Auth(string characterRefreshToken)
        {
            SsoToken token = connector.SSO.GetToken(GrantType.AuthorizationCode, characterRefreshToken);
            AuthorizedCharacterData auth_char = connector.SSO.Verify(token);
            connector.SetCharacterData(auth_char);
        }

        protected T Esi_ExecuteAndReturn<T>(EsiResponse<T> request) where T : class
        {
            //var response = request;
            //var responseDeserialize = JsonSerializer.Deserialize<T>(response.Message);

            if (!request.isSuccess)
                throw new Exception("Requset Failed!");

            return request.Data;
        }

        protected string ConnectionStrByName(string connStringName)
        {
            return ConfigurationStatic.LoadConnectionString(connStringName);
        }

        protected DbContextOptions<TDbContext> LoadContextOptions<TDbContext>(string settingsName)
            where TDbContext: DbContext
        {
            DbContextOptions<TDbContext> options = EveZoneDbOptions.LoadConnection<TDbContext>(settingsName);
            return options;
        }

        /// <summary>
        /// Очистка всех таблиц от данных
        /// </summary>
        public void EF_TruncateTables(params string[] truncated_types)
        {
            using var dbContext = new PublicContext(_publicContextOptions);
            var types = dbContext.Model.GetEntityTypes().ToList();
            foreach (var truncated_type in truncated_types) {
                var type = types.FirstOrDefault(x => x.Name.Split('.').Last() == truncated_type);
                if (type != null)
                {
                    var tableName = type.GetTableName();
                    if (tableName.Contains("EFMigrationsHistory"))
                        continue;

                    string sql = $"DELETE from \"{tableName}\"";
                    dbContext.Database.ExecuteSqlRaw(sql);
                }
            }
        }

        public void EF_Add<T>(params T[] items)
            where T: class
        {
            using var dbContext = new PublicContext(_publicContextOptions);
            dbContext.Set<T>().AddRange(items);
            dbContext.BulkSaveChanges();
        }
    }
    public static class EveZoneDbOptions
    {
        public static DbContextOptions<TDbContext> LoadConnection<TDbContext>(string settingName)
            where TDbContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            var conString = ConfigurationStatic.LoadConnectionString(settingName);
            optionsBuilder = optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder = optionsBuilder.UseNpgsql(conString, x => {
                //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                x.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });

            return optionsBuilder.Options;
        }
    }
}
