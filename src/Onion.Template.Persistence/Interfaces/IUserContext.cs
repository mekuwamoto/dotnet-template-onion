using Microsoft.EntityFrameworkCore;
using Onion.Template.Domain.Entities;

namespace Onion.Template.Persistence.Interfaces;

public interface IUserContext
{
    DbSet<User> Users { get; set; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
