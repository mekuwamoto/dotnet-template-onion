using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Todos.Requests;

public class EditTodoRequest
{
	[Required]
	public string Title { get; set; } = null!;
}
