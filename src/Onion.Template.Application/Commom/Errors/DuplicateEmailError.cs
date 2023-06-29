using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Commom.Errors;

public class DuplicateEmailError : IError
{
	public List<IError> Reasons => throw new NotImplementedException();
	public string Message => "Email already registered";
	public Dictionary<string, object> Metadata => new()
	{
		{ "StatusCode" ,HttpStatusCode.BadRequest },
	};
}
