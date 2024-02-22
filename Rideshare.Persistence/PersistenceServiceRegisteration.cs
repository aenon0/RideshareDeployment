using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Core.Configuration;
using Rideshare.Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MongoDB.Driver;
using Rideshare.Application.Contracts.Persistence;
using Rideshare_backend;
using Rideshare.Persistence.Repository;



namespace Rideshare.Persistence;

public static class PersistenceServiceRegisteration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = configuration.GetConnectionString("MongoConnectionString");
            return new MongoClient(connectionString);
        });
        var databaseName = configuration.GetValue<string>("MongoDatabaseName");
        services.AddScoped(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            return mongoClient.GetDatabase(databaseName);
        });
        // services.AddScoped(sp =>
        // {
        //     var mongoClient = sp.GetRequiredService<IMongoClient>();
        //     var databaseName = configuration.GetValue<string>("MongoDatabaseName");
        //     return new MongoDbContext(mongoClient, databaseName);
        // });
        services.AddScoped<MongoDbContext>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRiderRepository, RiderRepository>();
        services.AddScoped<IOtpRepository, OtpRepository>();

        return services;

    }
}
