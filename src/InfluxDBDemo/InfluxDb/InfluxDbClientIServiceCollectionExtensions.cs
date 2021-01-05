using InfluxData.Net.InfluxDb;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.InfluxDb
{
    public static class InfluxDbClientIServiceCollectionExtensions
    {
        public static IServiceCollection AddInfluxDbClient(this IServiceCollection services, Action<InfluxDbClientOptions> optionsBuilder)
        {
            services.Configure(optionsBuilder);

            services.AddSingleton<IInfluxDbClientFactory, InfluxDbClientFactory>();
            services.AddScoped<IInfluxDbClient, InfluxDbClientDecorator>(serviceProvider => serviceProvider.GetService<IInfluxDbClientFactory>().CreateClient());

            return services;
        }
    }
}
