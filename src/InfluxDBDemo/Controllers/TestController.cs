using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.Models;
using InfluxDBDemo.InfluxDb;
using InfluxDBDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IInfluxDbClient _influxDbClient;

        public TestController(IInfluxDbClient influxDbClient)
        {
            _influxDbClient = influxDbClient;
        }

        public async Task<IActionResult> Get()
        {
            //var person = new Person
            //{
            //    Id = 3,
            //    Age = 24,
            //    Name = "王五"
            //};
            //await _influxDbClient.Client.AddAsync(person);
            var result = await _influxDbClient.Client.GetListAsync<Person>("SELECT * FROM Person");
            return Ok();
        }
    }
}
