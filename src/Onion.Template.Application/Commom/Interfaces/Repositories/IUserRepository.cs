using Onion.Template.Domain.Entities;

namespace Onion.Template.Application.Commom.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
	Task<User?> GetUserByEmail(string email);
	Task<bool> IsEmailRegistered(string email);
}
