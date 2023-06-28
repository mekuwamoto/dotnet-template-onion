using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Validators;
using Onion.Template.Application.Commom.Settings;
using Onion.Template.Application.Services.Authentication;
using Onion.Template.Application.Users.Requests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
