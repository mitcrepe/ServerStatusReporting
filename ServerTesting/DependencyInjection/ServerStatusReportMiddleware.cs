using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ServerStatusReporting.ServerTesting.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServerStatusReporting.ServerTesting.DependencyInjection
{
    public class ServerStatusReportMiddleware
    {
        private readonly ServerDependenciesTester _tester;
        private readonly RequestDelegate _next;
        private readonly ServerStatusReportOptions _options;

        public ServerStatusReportMiddleware(RequestDelegate next, IOptions<ServerStatusReportOptions> options, ServerDependenciesTester tester)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next;
            _options = options.Value;
            _tester = tester;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments(_options.TestPath))
            {
                await _next(context);
                return;
            }
            
            ServerStatusReport report;

            if (context.Request.Path.Value.EndsWith("simple"))
            {
                report = _tester.SimpleReport();
            }
            else 
            {
                report = await _tester.TestServices(_options.Services);
            }

            JsonSerializerOptions jsonOptions = new();
            //jsonOptions.Converters.Add(new JsonStringEnumConverter());
            jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            string result = JsonSerializer.Serialize(report, jsonOptions);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
            return;
        }
    }
}
