﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.InfluxDb
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TagAttribute : Attribute
    {
    }
}
