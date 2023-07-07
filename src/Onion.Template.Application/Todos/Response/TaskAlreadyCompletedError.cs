using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Todos.Response;

public class TaskAlreadyCompletedError : IError
{
	public List<IError> Reasons => throw new NotImplementedException();

	public string Message => "Task already completed.";

	public Dictionary<string, object> Metadata => new()
	{
		{ "StatusCode" ,HttpStatusCode.BadRequest},
	};
}
