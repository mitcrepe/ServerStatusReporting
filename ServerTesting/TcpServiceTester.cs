using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStatusReporting.ServerTesting
{
    public class TcpServiceTester : ServiceTester
    {
        private readonly string _host;
        private readonly int _port;

        public override ServiceType Type => ServiceType.Tcp;

        public TcpServiceTester(string host, int port)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            _host = host;
            _port = port;
        }

        protected override Dictionary<string, string> GetDetails()
        {
            return new Dictionary<string, string>
            {
                { "Host", _host },
                { "Port", _port.ToString() }
            };
        }

        protected override Task<bool> SendRequest()
        {
            throw new NotImplementedException();
        }
    }
}
