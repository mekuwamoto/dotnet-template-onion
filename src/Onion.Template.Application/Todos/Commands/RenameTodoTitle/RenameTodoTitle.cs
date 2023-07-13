using AutoMapper;
using FluentResults;
using MediatR;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Todos.Requests;
using Onion.Template.Application.Todos.Response;
using Onion.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Todos.Commands.RenameTodoTitle;

public struct RenameTodoTitleCommand : IRequest<Result<TodoResponse>>
{
	public RenameTodoTitleCommand(Guid todoId, EditTodoRequest request)
	{
		TodoId = todoId;
		Request = request;
	}
	public Guid TodoId { get; set; }
	public EditTodoRequest Request { get; set; }
}

public class RenameTodoTitleHandler : IRequestHandler<RenameTodoTitleCommand, Result<TodoResponse>>
{
	private readonly ITodoRepository _repository;
	private readonly IUserAccessor _userAccessor;
	private readonly IMapper _mapper;

	public RenameTodoTitleHandler(ITodoRepository repository, IUserAccessor userAccessor, IMapper mapperr)
	{
		_repository = repository;
		_userAccessor = userAccessor;
		_mapper = mapperr;
	}

	public async Task<Result<TodoResponse>> Handle(RenameTodoTitleCommand command, CancellationToken cancellationToken)
	{
		Todo? todo = await _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId);
		if (todo == null)
			return Result.Fail<TodoResponse>(new NotFoundTodoError());
		todo.RenameTitle(command.Request.Title);
		await _repository.UpdateAsync(todo);
		return _mapper.Map<Todo?, TodoResponse>(todo);
	}
}