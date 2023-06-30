using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onion.Template.Application.Commom.Settings;
using System.Reflection;

namespace Onion.Template.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager config)
	{
		services.Configure<HashSettings>(config.GetSection(HashSettings.Section));
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		return services;
	}
}
