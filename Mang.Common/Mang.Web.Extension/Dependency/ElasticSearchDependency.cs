using System;
using Elasticsearch.Net;
using Mang.Public.Util;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;

namespace Mang.Web.Extension.Dependency
{
    public static class ElasticSearchDependency
    {
        public static void AddElasticSearch(this IServiceCollection services)
        {
            var connectionString = Appsettings.app("ConnectionStrings", "ElasticSearchConnectionString");

            var uri = new Uri(connectionString);
            var connPool = new SingleNodeConnectionPool(uri);

            /* 自定义时区出来
               NEST的序列化器默认认为我们的DateTime类型是UTC时区的，
               序列化时把时区信息丢失了，
               而我们的Kibana设置的东八区。
               所以显示的时候直接在原有时间上加了8个小时。*/
            IElasticsearchSerializer Customer(IElasticsearchSerializer builtin, IConnectionSettingsValues values)
                => new JsonNetSerializer(builtin, values, () => new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });


            var settings = new ConnectionSettings(connPool, Customer);
            settings.RequestTimeout(TimeSpan.FromMinutes(2));
            var elasticClient = new ElasticClient(settings);

            services.AddScoped<IElasticClient>(_ => elasticClient);
        }
    }
}