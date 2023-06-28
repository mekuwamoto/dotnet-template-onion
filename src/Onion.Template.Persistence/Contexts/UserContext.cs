using Microsoft.EntityFrameworkCore;
using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Domain.Entities;
using Onion.Template.Persistence.Interfaces;
using Onion.Template.Persistence.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Persistence.Contexts;

[Injection(DI.Scoped)]
public class UserContext : DbContext, IUserContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

	public async Task SaveChangesAsync()
	{
        await SaveChangesAsync();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
