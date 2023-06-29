using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Template.Application.Users.Response.Successful;

namespace Onion.Template.Api.Controllers.Commom;

[ApiController]
public abstract class BaseController : ControllerBase
{
	protected readonly IMediator _mediator;
	protected BaseController(IMediator mediator) => _mediator = mediator;

	protected IActionResult ReturnError(IError error)
		=> Problem(title: error.Message, statusCode: (int)error.Metadata["StatusCode"]);
}
