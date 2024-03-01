using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Core.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MongoDB.Driver;
using Rideshare.Application.Contracts.Persistence;
using Rideshare_backend;
using Rideshare.Persistence.Repositories;

namespace Rideshare.Persistence;

public static class PersistenceServiceRegisteration
{
    public static async Task<IServiceCollection> AddPersistenceAsync(this IServiceCollection services, IConfiguration configuration)
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
            var database = mongoClient.GetDatabase(databaseName);
            var collectionNames = database.ListCollectionNames().ToList();
            if (!collectionNames.Contains("Package"))
            {
                database.CreateCollection("Package");
            };
            if (!collectionNames.Contains("RiderHistory"))
            {
                database.CreateCollection("RiderHistory");
            };
            if (!collectionNames.Contains("RiderLocation"))
            {
                database.CreateCollection("RiderLocation");
            };
            return database;
        });
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRiderRepository, RiderRepository>();
        services.AddScoped<IOtpRepository, OtpRepository>();
        services.AddScoped<IPackageRepository, PackageRepository>();
        services.AddScoped<IRiderHistoryRepository, RiderHistoryRepository>();
        services.AddScoped<IRiderLocationRepository, RiderLocationRepository>();

        return services;

    }
}
