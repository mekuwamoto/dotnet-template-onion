using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Onion.Template.Api.Controllers.Commom;

[Route("/error")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
	public IActionResult Error()
	{
		return Problem();
	}

}
