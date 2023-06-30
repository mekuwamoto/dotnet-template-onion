using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onion.Template.Infrastructure.Settings;

namespace Onion.Template.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
    {
        services.Configure<JwtSettings>(config.GetSection(JwtSettings.Section));
        return services;
    }
}