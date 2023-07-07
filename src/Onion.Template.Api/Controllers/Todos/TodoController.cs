using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Template.Api.Controllers.Commom;
using Onion.Template.Application.Todos.Commands.CompleteTodoItem;
using Onion.Template.Application.Todos.Commands.CreateTodo;
using Onion.Template.Application.Todos.Queries.GetSingleTodo;
using Onion.Template.Application.Todos.Queries.GetTodos;
using Onion.Template.Application.Todos.Requests;

namespace Onion.Template.Api.Controllers.Todos;

[Route("api/todo")]
public class TodoController : BaseController
{
	public TodoController(IMediator mediator) : base(mediator) { }

	[HttpPost, Authorize]
	public async Task<IActionResult> CreateTodoItem([FromBody] CreateTodoRequest dto)
	{
		var response = await _mediator.Send(new CreateTodoCommand(dto));
		return Created($"{Request.Scheme}://{Request.Host.ToUriComponent()}{Request.Path.ToString()}/{response.TodoId}", response);
	}

	[HttpGet]
	public async Task<IActionResult> GetTodos()
	{
		var response = await _mediator.Send(new GetTodosQuery());
		return Ok(response);
	}

	[HttpGet("{todoId}")]
	public async Task<IActionResult> GetTodoById(Guid todoId)
	{
		var response = await _mediator.Send(new GetSingleTodoQuery(todoId));

		return response.IsSuccess ?
			Ok(response.Value) :
			ReturnError(response.Errors.First());
	}

	[HttpPost("complete/{todoId}")]
	public async Task<IActionResult> CompleteTodoItem(Guid todoId)
	{
		var response = await _mediator.Send(new CompleteTodoItemCommand(todoId));

		return response.IsSuccess ?
			Ok(response.Value) :
			ReturnError(response.Errors.First());
	}

}