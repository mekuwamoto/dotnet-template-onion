using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Template.Api.Controllers.Commom;
using Onion.Template.Application.Todos.Commands.CreateTodo;
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
		return Ok(response);
	}
}