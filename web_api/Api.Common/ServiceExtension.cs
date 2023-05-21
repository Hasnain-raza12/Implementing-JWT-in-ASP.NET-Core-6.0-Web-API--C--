using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Api.Common.Settings;

namespace Api.Common
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            
            services.AddSingleton(services =>
            {
                var configService = services.GetService<IConfiguration>();
                var mongoSetting = configService.GetSection(nameof(MongoSettings)).Get<MongoSettings>();

                var dbsetiing = configService.GetSection(nameof(MongoDb)).Get<MongoDb>();


                 var mongoClient = new MongoClient("mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&appName=mongosh+1");
               // var mongoClient = new MongoClient(mongoSetting.connectionstring);
                 var _database = mongoClient.GetDatabase("customers_db");
               // var _database = mongoClient.GetDatabase(dbsetiing.database);
                return _database;
            });
           
            return services;
        }
        public static IServiceCollection AddRepositry<T>(this IServiceCollection services,string collectionName) where T:IEntity
        {
            services.AddSingleton<IRepositry<T>>(services =>
            {
                var dbservice = services.GetService<IMongoDatabase>();

                return new Repositry<T>(dbservice,collectionName);
            });
            return services;

        }
    }
}
