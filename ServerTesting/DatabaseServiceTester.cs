using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStatusReporting.ServerTesting
{
    public class DatabaseServiceTester : ServiceTester
    {
        public override ServiceType Type => ServiceType.Database;

        protected override Dictionary<string, string> GetDetails()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> SendRequest()
        {
            throw new NotImplementedException();
        }
    }
}
