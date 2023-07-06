using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Todos.Requests;

public class CreateTodoRequest
{
	public string Title { get; set; } = null!;
}
