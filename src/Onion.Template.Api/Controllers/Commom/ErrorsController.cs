using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Onion.Template.Api.Controllers.Commom;

[Route("/error")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : BaseController
{
    public IActionResult Error()
    {
        return Problem();
    }

}
