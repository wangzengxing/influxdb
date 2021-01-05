using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.ClientModules;
using InfluxData.Net.InfluxDb.RequestClients;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.InfluxDb
{
    public class InfluxDbClientDecorator : IInfluxDbClient
    {
        private IInfluxDbClient _influxDbClient;

        public InfluxDbClientDecorator(IInfluxDbClient influxDbClient, InfluxDbClientOptions options)
        {
            _influxDbClient = influxDbClient;
            Client = new BasicClientModuleDecorator(_influxDbClient.Client, options);

            if (!string.IsNullOrEmpty(options.DbName))
            {
                EnsureDatabaseCreated(options.DbName);
            }

            if (options.DefaultRetentionPolicy != null)
            {
                EnsureRetentionPolicyCreated(options.DefaultRetentionPolicy, options.DbName);
            }
        }

        private void EnsureRetentionPolicyCreated(RetentionPolicy defaultPolicy, string dbName)
        {
            var policies = Retention.GetRetentionPoliciesAsync(dbName).Result;
            if (!policies.Any(r => r.Name == defaultPolicy.Name))
            {
                var result = Retention.CreateRetentionPolicyAsync(dbName, defaultPolicy.Name, defaultPolicy.Duration, defaultPolicy.ReplicationCopies).Result;
                if (!result.Success)
                {
                    throw new InvalidOperationException("初始化策略失败");
                }
            }
        }

        private void EnsureDatabaseCreated(string dbName)
        {
            var databaseNames = Database.GetDatabasesAsync().Result;
            if (!databaseNames.Any(r => r.Name == dbName))
            {
                Database.CreateDatabaseAsync(dbName);
            }
        }

        public IBasicClientModule Client { get; }

        public ISerieClientModule Serie => _influxDbClient.Serie;

        public IDatabaseClientModule Database => _influxDbClient.Database;

        public IRetentionClientModule Retention => _influxDbClient.Retention;

        public ICqClientModule ContinuousQuery => _influxDbClient.ContinuousQuery;

        public IDiagnosticsClientModule Diagnostics => _influxDbClient.Diagnostics;

        public IUserClientModule User => _influxDbClient.User;

        public IInfluxDbRequestClient RequestClient => _influxDbClient.RequestClient;
    }
}
