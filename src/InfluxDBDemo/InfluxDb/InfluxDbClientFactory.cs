using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.InfluxDb
{
    public class InfluxDbClientFactory : IInfluxDbClientFactory
    {
        private readonly InfluxDbClientOptions _options;

        public InfluxDbClientFactory(IOptions<InfluxDbClientOptions> optionsAccesser)
        {
            _options = optionsAccesser.Value;
        }

        public InfluxDbClientDecorator CreateClient()
        {
            var influxDbClient = new InfluxDbClient(_options.Url, _options.User, _options.Pwd, InfluxDbVersion.Latest);
            var clientDecorator = new InfluxDbClientDecorator(influxDbClient, _options);

            return clientDecorator;
        }
    }
}
