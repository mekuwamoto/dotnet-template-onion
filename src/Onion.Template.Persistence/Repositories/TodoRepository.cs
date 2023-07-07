using Microsoft.EntityFrameworkCore;
using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Domain.Entities;
using Onion.Template.Persistence.Interfaces;

namespace Onion.Template.Persistence.Repositories;

[Injection(DI.Scoped)]
public class TodoRepository : ITodoRepository
{
	private readonly IUserContext _context;

	public TodoRepository(IUserContext context)
		=> _context = context;

	public async Task<Todo> AddAsync(Todo todo)
	{
		await _context.Todos.AddAsync(todo);
		await _context.SaveChangesAsync();
		return todo;
	}

	public async Task<Todo> UpdateAsync(Todo todo)
	{
		_context.Todos.Update(todo);
		await _context.SaveChangesAsync();
		return todo;
	}

	public async Task<IReadOnlyList<Todo>> GetAllTodosFromUser(Guid idUser)
		=> await _context.Todos.AsNoTracking()
			.Where(td => td.UserId == idUser)
			.ToListAsync();

	public async Task<Todo?> GetTodoFromUser(Guid idUser, Guid todoId)
		=> await _context.Todos.FirstOrDefaultAsync(td => td.Id == todoId && td.UserId == idUser);

	public Task DeleteAsync(Guid id) => throw new NotImplementedException();
	public Task<IReadOnlyList<Todo>> GetAllAsync() => throw new NotImplementedException();
	public Task<Todo> GetByIdAsync(Guid id) => throw new NotImplementedException();

}
