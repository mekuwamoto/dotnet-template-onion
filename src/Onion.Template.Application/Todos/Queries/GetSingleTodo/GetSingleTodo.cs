using AutoMapper;
using FluentResults;
using MediatR;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Todos.Response;
using Onion.Template.Application.Users.Response.Errors;
using Onion.Template.Application.Users.Response.Successful;
using Onion.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Todos.Queries.GetSingleTodo;

public struct GetSingleTodoQuery : IRequest<Result<TodoResponse>>
{
	public Guid TodoId { get; set; }
	public GetSingleTodoQuery(Guid todoId) => TodoId = todoId;
}

public class GetSingleTodoHandler : IRequestHandler<GetSingleTodoQuery, Result<TodoResponse>>
{
	private readonly ITodoRepository _repository;
	private readonly IUserAccessor _userAccessor;
	private readonly IMapper _mapper;

	public GetSingleTodoHandler(ITodoRepository repository, IUserAccessor userAccessor, IMapper mapper)
	{
		_repository = repository;
		_userAccessor = userAccessor;
		_mapper = mapper;
	}

	public async Task<Result<TodoResponse>> Handle(GetSingleTodoQuery query, CancellationToken cancellationToken)
	{
		Todo? todo = await _repository.GetTodoFromUser(_userAccessor.UserId, query.TodoId);
		if (todo == null)
			return Result.Fail<TodoResponse>(new NotFoundTodoError());
		return _mapper.Map<Todo?, TodoResponse>(todo);
	}
}
