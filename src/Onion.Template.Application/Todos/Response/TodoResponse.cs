using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Todos.Response;

public class TodoResponse
{
    public Guid TodoId { get; set; }
    public string Title { get; set; } = null!;
    public bool Completed { get; set; }
}
