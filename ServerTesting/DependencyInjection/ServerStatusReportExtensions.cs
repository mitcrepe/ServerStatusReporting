using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using System.Net.Http;

namespace ServerStatusReporting.ServerTesting.DependencyInjection
{
    public static class ServerStatusReportExtensions
    {
        public static IServiceCollection AddServerStatusReport(this IServiceCollection services, Action<ServerStatusReportOptions> configureOptions)
        {
            services.AddSingleton<ServerDependenciesTester>();
            services.Configure(configureOptions);
            return services;
        }

        public static IApplicationBuilder UseServerStatusReport(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ServerStatusReportMiddleware>();
        }


        // Return some authentication builder so you can set auth headers that HttpServiceDefinition will use.
        public static ServerStatusReportOptions AddHttpServiceTest(this ServerStatusReportOptions options, string url, HttpMethod method)
        {
            options.EnsureServices();
            options.Services.Add(new HttpServiceTester(url, method));
            return options;
        }

        public static ServerStatusReportOptions AddDatabaseTest(this ServerStatusReportOptions options, string conncetionString)
        {
            options.EnsureServices();

            //options.Services.Add(new HttpServiceDefinition(url, method));
            return options;
        }

        private static void EnsureServices(this ServerStatusReportOptions options)
        {
            if (options != null && options.Services == null)
            {
                options.Services = new List<ServiceTester>();
            }
        }
    }
}
