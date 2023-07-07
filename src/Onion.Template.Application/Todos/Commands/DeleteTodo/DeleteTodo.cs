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

namespace Onion.Template.Application.Todos.Commands.DeleteTodo;

public struct DeleteTodoCommand : IRequest<Result>
{
	public DeleteTodoCommand(Guid todoId) => TodoId = todoId;
	public Guid TodoId { get; set; }
}

public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, Result>
{
	private readonly ITodoRepository _repository;
	private readonly IUserAccessor _userAccessor;

	public DeleteTodoHandler(ITodoRepository repository, IUserAccessor userAccessor)
	{
		_repository = repository;
		_userAccessor = userAccessor;
	}

	public async Task<Result> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
	{
		Todo? todo = await _repository.GetTodoFromUser(_userAccessor.UserId, request.TodoId);
		if (todo == null)
			return Result.Fail(new NotFoundTodoError());

		await _repository.DeleteAsync(todo);
		return Result.Ok();
	}
}