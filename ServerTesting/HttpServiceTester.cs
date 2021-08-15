using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServerStatusReporting.ServerTesting
{
    public class HttpServiceTester : ServiceTester
    {
        private static readonly HttpClient _client = new HttpClient();

        private readonly string _url;
        private readonly HttpMethod _method;

        public override ServiceType Type { get => ServiceType.Http; }

        public HttpServiceTester(string url, HttpMethod method)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException($"'{nameof(url)}' cannot be null or empty.", nameof(url));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            _url = url;
            _method = method;
        }

        protected async override Task<bool> SendRequest()
        {
            UriBuilder builder = new UriBuilder(_url);
            HttpRequestMessage request = new(_method, builder.Uri.AbsoluteUri);
            var response = await _client.SendAsync(request);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        protected override Dictionary<string, string> GetDetails()
        {
            return new Dictionary<string, string>
            {
                { "Method", _method.Method },
                { "URL", _url }
            };
        }
    }
}
