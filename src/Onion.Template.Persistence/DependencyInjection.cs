using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onion.Template.Persistence.Contexts;

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