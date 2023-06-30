using FluentResults;
using System.Net;

namespace Onion.Template.Application.Users.Response.Errors;

public class DuplicateEmailError : IError
{
	public List<IError> Reasons => throw new NotImplementedException();
	public string Message => "Email already registered";
	public Dictionary<string, object> Metadata => new()
	{
		{ "StatusCode" ,HttpStatusCode.BadRequest },
	};
}
