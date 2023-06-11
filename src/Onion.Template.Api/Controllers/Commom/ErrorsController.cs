using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Onion.Template.Api.Controllers.Commom;

[Route("/error")]
public class ErrorsController : BaseController
{
    public IActionResult Error()
    {
        return Problem();
    }

}
