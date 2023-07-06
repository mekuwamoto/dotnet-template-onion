using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Todos.Response;

public class NotFoundTodoError : IError
{
	public List<IError> Reasons => throw new NotImplementedException();

	public string Message => "Todo does not exists";

	public Dictionary<string, object> Metadata => new()
	{
		{ "StatusCode" ,HttpStatusCode.NotFound},
	};
}
