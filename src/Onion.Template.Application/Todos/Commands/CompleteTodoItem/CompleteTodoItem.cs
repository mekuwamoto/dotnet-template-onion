using AutoMapper;
using FluentResults;
using MediatR;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Todos.Response;
using Onion.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Todos.Commands.CompleteTodoItem;

public struct CompleteTodoItemCommand : IRequest<Result<TodoResponse>>
{
	public CompleteTodoItemCommand(Guid todoId) => TodoId = todoId;
	public Guid TodoId { get; set; }
}


public class CompleteTodoItemHandler : IRequestHandler<CompleteTodoItemCommand, Result<TodoResponse>>
{
	private readonly ITodoRepository _repository;
	private readonly IUserAccessor _userAccessor;
	private readonly IMapper _mapper;

	public CompleteTodoItemHandler(ITodoRepository repository, IUserAccessor userAccessor, IMapper mapper)
	{
		_repository = repository;
		_userAccessor = userAccessor;
		_mapper = mapper;
	}

	public async Task<Result<TodoResponse>> Handle(CompleteTodoItemCommand command, CancellationToken cancellationToken)
	{
		Todo? todo = await _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId);
		if (todo is null)
			return Result.Fail<TodoResponse>(new NotFoundTodoError());
		if (todo.Completed)
			return Result.Fail<TodoResponse>(new TaskAlreadyCompletedError());
		todo.CompleteTask();
		await _repository.UpdateAsync(todo);
		return _mapper.Map<Todo, TodoResponse>(todo);
	}
}