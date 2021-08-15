using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServerStatusReporting.ServerTesting.Reporting
{
    public class ServerStatusReport
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReportType Type { get; set; }
        public ReportStatus Status { get; set; }
        public List<ServiceTestResult> Services { get; set; }
    }
}
