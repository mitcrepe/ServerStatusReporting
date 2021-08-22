using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStatusReporting.ServerTesting
{
    public class DatabaseServiceTester : ServiceTester
    {
        private readonly string _connectionString;

        public override ServiceType Type => ServiceType.Database;

        public DatabaseServiceTester(string connectionString)
        {
            if (connectionString is null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        protected override Dictionary<string, string> GetDetails()
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            string[] sourceAndPort = connection.DataSource.Split(',');

            string source = sourceAndPort[0];
            string port = sourceAndPort.Length > 1 ? sourceAndPort[1] : string.Empty;
            string database = connection.Database;

            return new Dictionary<string, string>
            {
                { "Host", source },
                { "Port", port },
                { "DbName", database }
            };
        }

        protected override async Task<bool> SendRequest()
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await connection.CloseAsync();
            return true;
        }
    }
}
