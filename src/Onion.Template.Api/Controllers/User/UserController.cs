using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Template.Api.Controllers.Commom;

namespace Onion.Template.Api.Controllers.User;

[Route("api/user")]
public class UserController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok("aaa");
    }

}
