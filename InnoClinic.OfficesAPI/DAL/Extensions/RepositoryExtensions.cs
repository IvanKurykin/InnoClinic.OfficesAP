using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DAL.Entities;
using MongoDB.Driver;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(configuration["ConnectionStrings:DatabaseName"]);

        services.AddSingleton<IMongoDatabase>(database);
        services.AddScoped<IMongoCollection<Office>>(sp =>
            sp.GetRequiredService<IMongoDatabase>().GetCollection<Office>("offices"));

        services.AddScoped<IOfficeRepository, OfficeRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();

        return services;
    }
}