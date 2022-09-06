using eveDirect.Shared.ConfigurationHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using Winton.Extensions.Configuration.Consul;

namespace eveDirect.Shared.GeneralTest
{
    public class FunctionalTestCore</*Scenario,*/ TStartup>
        //where Scenario : class
        where TStartup : class
    {
        protected TestServer CreateServer()
        {
            var hostBuilder = new WebHostBuilder()
                .ConfigureAppConfiguration(builder =>
                {
                    // Consul
                    builder.AddConsul("eveDirect", options => 
                        options.ConsulConfigurationOptions = cco =>
                            cco.Address = new Uri(ConfigurationStatic.ConsulAddress)
                    );

                    builder.AddEnvironmentVariables();
                })
                .UseStartup<TStartup>();

            var testServer = new TestServer(hostBuilder);

            return testServer;
        }
    }
}
