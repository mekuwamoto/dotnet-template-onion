using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Onion.Template.Api.Controllers.Commom;

[ApiController]
public abstract class BaseController : ControllerBase
{
	protected readonly IMediator _mediator;
	protected BaseController(IMediator mediator) => _mediator = mediator;

	protected IActionResult ReturnError(IError error)
		=> Problem(title: error.Message, statusCode: (int)error.Metadata["StatusCode"]);
}
