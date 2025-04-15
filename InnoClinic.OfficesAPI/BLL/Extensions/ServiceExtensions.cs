using BLL.Interfaces;
using BLL.Mapper;
using BLL.Services;
using DAL.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddBusinessLogiсLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessLayerServices(configuration);

        services.AddScoped<IOfficeService, OfficeService>();

        services.AddAutoMapper(typeof(OfficeProfile));

        return services;
    }
}