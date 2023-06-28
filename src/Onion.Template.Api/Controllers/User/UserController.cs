using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Onion.Template.Api.Controllers.Commom;
using Onion.Template.Application.Users.Commands.Login;
using Onion.Template.Application.Users.Commands.Register;
using Onion.Template.Application.Users.Requests;

namespace Onion.Template.Api.Controllers.User;

[Route("api/user")]
public class UserController : BaseController
{
	public UserController(IMediator mediator) : base(mediator)
	{

	}

	[HttpPost("signup")]
	public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
	{
		var response = await _mediator.Send(new RegisterCommand(request));
		return Ok(response);
	}

	[HttpPost("signin")]
	public async Task<IActionResult> Login([FromBody] LoginRequest request)
	{
		var response = await _mediator.Send(new LoginCommand(request));
		return Ok(response);
	}
}
