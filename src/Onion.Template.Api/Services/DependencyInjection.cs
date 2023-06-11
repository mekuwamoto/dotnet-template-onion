using Onion.Template.Application;
using Onion.Template.Infrastructure;
using Onion.Template.Persistence;

namespace Onion.Template.Api.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager config)
        => services
            .AddApplication(config)
            .AddPersistence(config)
            .AddInfrastructure(config)
            .AddApiServices();

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}
