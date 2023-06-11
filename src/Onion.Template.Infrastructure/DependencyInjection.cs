using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Infrastructure.Authentication;
using Onion.Template.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
    {
        services.Configure<JwtSettings>(config.GetSection(JwtSettings.Section));
        return services;
    }
}