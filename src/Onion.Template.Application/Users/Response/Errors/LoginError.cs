using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
