using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.InfluxDb
{
    public class RetentionPolicy
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public int ReplicationCopies { get; set; }
    }
}
