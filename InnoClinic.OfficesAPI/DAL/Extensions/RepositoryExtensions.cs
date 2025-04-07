using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DAL.Entities;
using MongoDB.Driver;

namespace DAL.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(configuration["ConnectionStrings:DatabaseName"]);

        services.AddSingleton(database);
        services.AddScoped(provider => provider.GetRequiredService<IMongoDatabase>().GetCollection<Office>("offices"));

        return services;
    }
}