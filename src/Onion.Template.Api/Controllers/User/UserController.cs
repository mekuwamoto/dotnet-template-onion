using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Template.Api.Controllers.Commom;
using Onion.Template.Application.User.Commands.Register;

namespace Onion.Template.Api.Controllers.User;

[Route("api/user")]
public class UserController : BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        var response = await _mediator.Send(new RegisterCommand());
        return Ok(response);
    }

}
