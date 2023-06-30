using FluentResults;
using System.Net;

namespace Onion.Template.Application.Users.Response.Errors;

public class LoginError : IError
{
	public List<IError> Reasons => throw new NotImplementedException();
	public string Message => "Email or password did not match";
	public Dictionary<string, object> Metadata => new()
	{
		{ "StatusCode" ,HttpStatusCode.BadRequest },
	};
}
