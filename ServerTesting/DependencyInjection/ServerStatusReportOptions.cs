using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStatusReporting.ServerTesting.DependencyInjection
{
    public class ServerStatusReportOptions
    {
        public string TestPath { get; set; }
        public List<ServiceTester> Services { get; set; }
    }
}
