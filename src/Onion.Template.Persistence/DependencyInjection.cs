using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager config)
    {
        var connectionString = config.GetConnectionString("ConnectionSqlServer");
        services.AddDbContext<UserContext>(x => x.UseSqlServer(connectionString));
        return services;
    }
}