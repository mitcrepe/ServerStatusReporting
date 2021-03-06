using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServerStatusReporting.ServerTesting
{
    public class ServiceTestResult
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ServiceType Type { get; set; }
        public bool Ok { get; set; }
        public Dictionary<string, string> Details { get; set; }
        public bool IsCritical { get; set; }
    }
}
