using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Onion.Template.Application;
using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Infrastructure;
using Onion.Template.Persistence;
using System.Reflection;
using System.Text;

namespace Onion.Template.Api.Services;

public static class DependencyInjection
{
	public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager config)
	{
		services.AddApplication(config);
		services.AddPersistence(config);
		services.AddInfrastructure(config);
		services.AddApiServices(config);
		services.RegisterServices(config);
		return services;
	}

	public static IServiceCollection AddApiServices(this IServiceCollection services, ConfigurationManager config)
	{
		var key = Encoding.ASCII.GetBytes(config.GetValue<string>("JwtSettings:Secret"));
		services.AddControllers();
		services.AddAuthentication(config =>
		{
			config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(config =>
		{
			config.RequireHttpsMetadata = false;
			config.SaveToken = false;
			config.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
			};
		});
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		return services;
	}

	public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		Type dependencyRegistration = typeof(InjectionAttribute);
		List<Type> types = AppDomain.CurrentDomain
			.GetAssemblies()
			.SelectMany(s => s.GetTypes())
			.Where(p => !p.IsInterface && p.IsDefined(typeof(InjectionAttribute), false))
			.ToList();
		types.ForEach(type => RegisterType(services, type));
	}

	public static void RegisterType(this IServiceCollection services, Type type)
	{
		Type? @interface = type.GetInterface($"I{type.Name}");
		if (@interface == null) return;
		InjectionAttribute? dependencyAttribute = type.GetCustomAttribute(typeof(InjectionAttribute), true) as InjectionAttribute;
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
