using AutoMapper;
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

namespace Onion.Template.Application.Todos.Queries.GetTodos;
public struct GetTodosQuery : IRequest<IReadOnlyList<TodoResponse>> { }

public class GetTodosHandler : IRequestHandler<GetTodosQuery, IReadOnlyList<TodoResponse>>
{
	private readonly ITodoRepository _repository;
	private readonly IUserAccessor _userAccessor;
	private readonly IMapper _mapper;

	public GetTodosHandler(ITodoRepository repository, IUserAccessor userAccessor, IMapper mapper)
	{
		_repository = repository;
		_userAccessor = userAccessor;
		_mapper = mapper;
	}

	public async Task<IReadOnlyList<TodoResponse>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
	{
		IReadOnlyList<Todo> todos = await _repository.GetAllTodosFromUser(_userAccessor.UserId);
		return _mapper.Map<IReadOnlyList<Todo>, IReadOnlyList<TodoResponse>>(todos);
	}
}