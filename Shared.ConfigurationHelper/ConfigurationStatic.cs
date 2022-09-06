using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Winton.Extensions.Configuration.Consul;
using System;

namespace eveDirect.Shared.ConfigurationHelper
{
    public static class ConfigurationStatic
    {
        public const string ConsulAddress = "http://consul.consul.svc.cluster.local:8500";

        public static string LoadConnectionString(string connStrName)
        {
            string identityConnectionStr = GetConfiguration().GetConnectionString($"{connStrName}");
            return identityConnectionStr;
        }

        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder().AddConsul("eveDirect", options =>
            {
                options.ConsulConfigurationOptions = cco =>
                {
                    cco.Address = new Uri(ConsulAddress);
                };
            }).Build();
        }
    }

    public static class ContextStatic
    {
        public static DbContextOptionsBuilder DbContextOptions(string connectionStr)
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            //builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            builder.UseNpgsql(connectionStr, npgsqlOptionsAction: sqlOptions =>
            {
                //sqlOptions.MigrationsAssembly(typeof(TStartUp).GetTypeInfo().Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });
            return builder;
        }
    }
}
