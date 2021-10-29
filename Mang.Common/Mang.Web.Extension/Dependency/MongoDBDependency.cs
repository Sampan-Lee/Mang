using System.Linq;
using Mang.Infrastructure;
using Mang.Public.Extension;
using Mang.Public.Util;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace Mang.Web.Extension.Dependency
{
    public static class MongoDBDependency
    {
        public static void AddMongoDB(this IServiceCollection services)
        {
            var host = Appsettings.app("MongoDBSettings", "Host");
            var port = Appsettings.app("MongoDBSettings", "Port").ToInt();
            var databaseName = Appsettings.app("MongoDBSettings", "DatabaseName");
            var userName = Appsettings.app("MongoDBSettings", "UserName");
            var password = Appsettings.app("MongoDBSettings", "Password");

            //增删改操作记录日志
            var interceptorCommandNames = new[] { "insert", "findAndModify", "update", "delete" };
            var mongoClient = new MongoClient(new MongoClientSettings
            {
                Server = new MongoServerAddress(host, port),
                Credential = MongoCredential.CreateCredential(databaseName, userName, password),

                ClusterConfigurator = builder => builder.Subscribe<CommandStartedEvent>(e =>
                {
                    if (interceptorCommandNames.Contains(e.CommandName))
                    {
                        LogHelper.Info($"MongoDB：{e.Command.ToJson()}");
                    }
                })
            });

            services.AddSingleton<IMongoClient>(mongoClient);
            //BsonSerializer.RegisterSerializer(DateTimeSerializer.LocalInstance);
        }
    }
}