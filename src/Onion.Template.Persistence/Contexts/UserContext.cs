using Microsoft.EntityFrameworkCore;
using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Domain.Entities;
using Onion.Template.Persistence.Interfaces;
using System.Reflection;

namespace Onion.Template.Persistence.Contexts;

[Injection(DI.Scoped)]
public class UserContext : DbContext, IUserContext
{
	public UserContext(DbContextOptions<UserContext> options) : base(options) { }

	public DbSet<User> Users { get; set; }
	public DbSet<Todo> Todos { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await base.SaveChangesAsync(cancellationToken);
	}
}
