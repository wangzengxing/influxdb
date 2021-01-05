using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.InfluxDb
{
    public class InfluxDbClientOptions
    {
        public string Url { get; set; }
        public string User { get; set; }
        public string Pwd { get; set; }
        public string DbName { get; set; }
        public RetentionPolicy DefaultRetentionPolicy { get; set; }
    }
}
