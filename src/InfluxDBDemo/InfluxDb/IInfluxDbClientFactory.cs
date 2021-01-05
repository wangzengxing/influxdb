using InfluxData.Net.InfluxDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.InfluxDb
{
    public interface IInfluxDbClientFactory
    {
        InfluxDbClientDecorator CreateClient();
    }
}
