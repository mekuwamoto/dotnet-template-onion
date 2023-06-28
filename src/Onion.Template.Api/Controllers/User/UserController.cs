using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Onion.Template.Api.Controllers.Commom;
using Onion.Template.Application.Users.Commands.Register;
using Onion.Template.Application.Users.Requests;
using Onion.Template.Application.Users.Validations;

namespace Onion.Template.Api.Controllers.User;

[Route("api/user")]
public class UserController : BaseController
{
	public UserController(IMediator mediator) : base(mediator)
	{

	}

	[HttpPost]
	public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
	{
		var response = await _mediator.Send(new RegisterCommand(request));
		return Ok(response);
	}

}
