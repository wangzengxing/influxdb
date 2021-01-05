using InfluxData.Net.InfluxDb.ClientModules;
using InfluxData.Net.InfluxDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfluxDBDemo.InfluxDb
{
    public static class IBasicClientModuleExtensions
    {
        public static async Task<bool> AddAsync<TEntity>(this IBasicClientModule basicClientModule, TEntity entity)
             where TEntity : class, new()
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var type = entity.GetType();
            var point = new Point
            {
                Name = type.Name,
                Tags = new Dictionary<string, object>(),
                Fields = new Dictionary<string, object>()
            };

            foreach (var property in type.GetProperties())
            {
                var tagAttribute = property.GetCustomAttributes(false).OfType<TagAttribute>().FirstOrDefault();
                if (tagAttribute != null)
                {
                    point.Tags.Add(property.Name, property.GetValue(entity));
                    continue;
                }

                point.Fields.Add(property.Name, property.GetValue(entity));
            }

            var response = await basicClientModule.WriteAsync(point);
            return response.Success;
        }

        public static async Task<(int, List<TEntity>)> GetListAsync<TEntity>(this IBasicClientModule basicClientModule, string querySql)
            where TEntity : class, new()
        {
            var totalCount = 0;
            var result = new List<TEntity>();

            var queries = new List<string> { querySql };
            var type = typeof(TEntity);

            var countProperty = type.GetProperties().FirstOrDefault(r => !r.GetCustomAttributes(false).OfType<TagAttribute>().Any());
            if (countProperty != null)
            {
                var countQuery = $"SELECT COUNT(\"{countProperty.Name}\") FROM {type.Name}";
                queries.Add(countQuery);
            }

            var series = await basicClientModule.QueryAsync(queries);

            var serie = series.FirstOrDefault();
            if (serie == null)
            {
                return (totalCount, result);
            }

            var countSerie = series.LastOrDefault();
            if (countSerie != null)
            {
                totalCount = Convert.ToInt32(countSerie.Values.FirstOrDefault().LastOrDefault());
            }

            foreach (var val in serie.Values)
            {
                var entity = new TEntity();

                for (int i = 0; i < serie.Columns.Count; i++)
                {
                    var column = serie.Columns[i];
                    if (column == "time")
                    {
                        continue;
                    }

                    var originValue = val[i];
                    var property = type.GetProperty(column);

                    var changedValue = Convert.ChangeType(originValue, property.PropertyType);
                    property.SetValue(entity, changedValue);
                }

                result.Add(entity);
            }

            return (totalCount, result);
        }
    }
}
