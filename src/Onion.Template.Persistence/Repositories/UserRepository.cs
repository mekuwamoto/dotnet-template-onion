using Microsoft.EntityFrameworkCore;
using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Domain.Entities;
using Onion.Template.Persistence.Interfaces;

namespace Onion.Template.Persistence.Repositories;

[Injection(DI.Scoped)]
public class UserRepository : IUserRepository
{
	private readonly IUserContext _context;

	public UserRepository(IUserContext context)
	{
		_context = context;
	}

	public async Task AddAsync(User entity)
	{
		await _context.Users.AddAsync(entity);
		await _context.SaveChangesAsync();
	}

	public async Task<User?> GetUserByEmail(string email)
		=> await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

	public async Task<bool> IsEmailRegistered(string email)
		=> await _context.Users.AnyAsync(user => user.Email == email);

	public Task<string> DeleteAsync(Guid id) => throw new NotImplementedException();
	public Task<IReadOnlyList<User>> GetAllAsync() => throw new NotImplementedException();
	public Task<User> GetByIdAsync(Guid id) => throw new NotImplementedException();
	public Task<string> UpdateAsync(User entity) => throw new NotImplementedException();
}
