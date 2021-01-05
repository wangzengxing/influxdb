using InfluxDBDemo.InfluxDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.Model
{
    public class Person
    {
        [Tag]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
