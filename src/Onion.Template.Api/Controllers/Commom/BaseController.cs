using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Onion.Template.Api.Controllers.Commom;

[ApiController]
public abstract class BaseController : ControllerBase
{
	protected readonly IMediator _mediator;
	protected BaseController(IMediator mediator) => _mediator = mediator;
}
