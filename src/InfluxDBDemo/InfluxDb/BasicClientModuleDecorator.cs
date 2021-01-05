using InfluxData.Net.Common.Infrastructure;
using InfluxData.Net.InfluxDb.ClientModules;
using InfluxData.Net.InfluxDb.Models;
using InfluxData.Net.InfluxDb.Models.Responses;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.InfluxDb
{
    public class BasicClientModuleDecorator : IBasicClientModule
    {
        private readonly IBasicClientModule _basicClientModule;
        private readonly InfluxDbClientOptions _clientOptions;

        public BasicClientModuleDecorator(IBasicClientModule basicClientModule, InfluxDbClientOptions clientOptions)
        {
            _basicClientModule = basicClientModule;
            _clientOptions = clientOptions;
        }

        public Task<IEnumerable<IEnumerable<Serie>>> MultiQueryAsync(IEnumerable<string> queries, string dbName = null, string epochFormat = null, long? chunkSize = null)
        {
            if (dbName == null)
            {
                dbName = _clientOptions.DbName;
            }

            return _basicClientModule.MultiQueryAsync(queries, dbName, epochFormat, chunkSize);
        }

        public Task<IEnumerable<Serie>> QueryAsync(string query, string dbName = null, string epochFormat = null, long? chunkSize = null)
        {
            if (dbName == null)
            {
                dbName = _clientOptions.DbName;
            }

            return _basicClientModule.QueryAsync(query, dbName, epochFormat, chunkSize);
        }

        public Task<IEnumerable<Serie>> QueryAsync(IEnumerable<string> queries, string dbName = null, string epochFormat = null, long? chunkSize = null)
        {
            if (dbName == null)
            {
                dbName = _clientOptions.DbName;
            }

            return _basicClientModule.QueryAsync(queries, dbName, epochFormat, chunkSize);
        }

        public Task<IEnumerable<Serie>> QueryAsync(string queryTemplate, object parameters, string dbName = null, string epochFormat = null, long? chunkSize = null)
        {
            if (dbName == null)
            {
                dbName = _clientOptions.DbName;
            }

            return _basicClientModule.QueryAsync(queryTemplate, parameters, dbName, epochFormat, chunkSize);
        }

        public Task<IInfluxDataApiResponse> WriteAsync(Point point, string dbName = null, string retentionPolicy = null, string precision = "ms")
        {
            if (dbName == null)
            {
                dbName = _clientOptions.DbName;
            }

            if (retentionPolicy==null)
            {
                retentionPolicy = _clientOptions.DefaultRetentionPolicy.Name;
            }

            return _basicClientModule.WriteAsync(point, dbName, retentionPolicy, precision);
        }

        public Task<IInfluxDataApiResponse> WriteAsync(IEnumerable<Point> points, string dbName = null, string retentionPolicy = null, string precision = "ms")
        {
            if (dbName == null)
            {
                dbName = _clientOptions.DbName;
            }

            if (retentionPolicy == null)
            {
                retentionPolicy = _clientOptions.DefaultRetentionPolicy.Name;
            }

            return _basicClientModule.WriteAsync(points, dbName, retentionPolicy, precision);
        }
    }
}
