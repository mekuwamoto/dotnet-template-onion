using Onion.Template.Domain.Entities;


namespace Onion.Template.Application.Commom.Interfaces.Repositories;

public interface ITodoRepository : IRepository<Todo>
{
	Task<IReadOnlyList<Todo>> GetAllTodosFromUser(Guid idUser);
	Task<Todo?> GetTodoFromUser(Guid idUser, Guid todoId);
}
