using AutoMapper;
using MediatR;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Todos.Requests;
using Onion.Template.Application.Todos.Response;
using Onion.Template.Domain.Entities;

namespace Onion.Template.Application.Todos.Commands.CreateTodo;

public struct CreateTodoCommand : IRequest<TodoResponse>
{
	public CreateTodoCommand(CreateTodoRequest request) => Request = request;
	public CreateTodoRequest Request { get; set; } = null!;
}

public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, TodoResponse>
{
	private readonly ITodoRepository _repository;
	private readonly IUserAccessor _accessor;
	private readonly IMapper _mapper;

	public CreateTodoHandler(ITodoRepository repository, IUserAccessor accessor, IMapper mapper)
	{
		_repository = repository;
		_accessor = accessor;
		_mapper = mapper;
	}

	public async Task<TodoResponse> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
	{
		Todo todo = new Todo(command.Request.Title, _accessor.UserId);
		Todo createdTodo = await _repository.AddAsync(todo);
		var response = _mapper.Map<Todo, TodoResponse>(createdTodo);
		return response;
	}
}