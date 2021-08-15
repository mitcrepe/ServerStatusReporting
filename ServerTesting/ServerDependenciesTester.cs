using ServerStatusReporting.ServerTesting.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerStatusReporting.ServerTesting
{
    public class ServerDependenciesTester
    {
        // Who will set totalstatusreport data? Report Type - simple, full?

        public async Task<ServerStatusReport> TestServices(List<ServiceTester> services)
        {
            ServerStatusReport result = new();
            result.Type = ReportType.Full;
            result.Services = new List<ServiceTestResult>();

            foreach (ServiceTester service in services)
            {
                ServiceTestResult serviceReport = await service.Test();
                result.Services.Add(serviceReport);
            }

            if (result.Services.Any(service => !service.Ok && service.IsCritical))
            {
                result.Status = ReportStatus.Error;
            }
            else if (result.Services.Any(service => !service.Ok))
            {
                result.Status = ReportStatus.NotOk;
            }
            else
            {
                result.Status = ReportStatus.Ok;
            }

            return result;
        }
    }
}
