using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStatusReporting.ServerTesting
{
    public abstract class ServiceTester
    {
        public abstract ServiceType Type { get; }

        protected abstract Task<bool> SendRequest();
        protected abstract Dictionary<string, string> GetDetails();

        public async Task<ServiceTestResult> Test()
        {
            ServiceTestResult result = new();
            result.Type = Type;

            result.Details = GetDetails();

            try
            {
                result.Ok = await SendRequest();
            }
            catch (Exception)
            {
                result.Ok = false;
            }

            return result;
        }
    }
}
