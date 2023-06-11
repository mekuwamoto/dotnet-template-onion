using Microsoft.IdentityModel.Tokens;
using Onion.Template.Application;
using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Infrastructure;
using Onion.Template.Persistence;
using System.Reflection;

namespace Onion.Template.Api.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddApplication(config);
        services.AddPersistence(config);
        services.AddInfrastructure(config);
        services.AddApiServices();
        services.RegisterServices(config);
        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        Type dependencyRegistration = typeof(DependencyAttribute);
        List<Type> types = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => !p.IsInterface && p.IsDefined(typeof(DependencyAttribute), false))
            .ToList();
        types.ForEach(type => RegisterType(services, type));
    }

    public static void RegisterType(this IServiceCollection services, Type type)
    {
        Type? @interface = type.GetInterface($"I{type.Name}");
        if (@interface == null) return;
        DependencyAttribute? dependencyAttribute = type.GetCustomAttribute(typeof(DependencyAttribute), true) as DependencyAttribute;
        DI? dependencyType = dependencyAttribute?.Di;
        switch (dependencyType)
        {
            case DI.Scoped:
                services.AddScoped(@interface, type);
                break;
            case DI.Singleton:
                services.AddSingleton(@interface, type);
                break;
            case DI.Transient:
                services.AddTransient(@interface, type);
                break;
            default:
                break;
        }
    }
}
