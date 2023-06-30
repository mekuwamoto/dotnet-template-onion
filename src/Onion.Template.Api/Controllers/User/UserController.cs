using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onion.Template.Api.Controllers.Commom;
using Onion.Template.Application.Users.Commands.Login;
using Onion.Template.Application.Users.Commands.Register;
using Onion.Template.Application.Users.Requests;

namespace Onion.Template.Api.Controllers.User;

[Route("api/user")]
public class UserController : BaseController
{
	public UserController(IMediator mediator) : base(mediator) { }

	[HttpPost("signup"), AllowAnonymous]
	public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
	{
		var response = await _mediator.Send(new RegisterCommand(request));

		return response.IsSuccess ?
			Ok(response.Value) :
			ReturnError(response.Errors.First());
	}

	[HttpPost("signin"), AllowAnonymous]
	public async Task<IActionResult> Login([FromBody] LoginRequest request)
	{
		var response = await _mediator.Send(new LoginCommand(request));

		return response.IsSuccess ?
			Ok(response.Value) :
			ReturnError(response.Errors.First());
	}
}
