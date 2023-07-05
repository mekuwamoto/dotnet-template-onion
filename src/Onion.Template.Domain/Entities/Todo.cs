using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Domain.Entities;

public class Todo : BaseEntity
{
	public Guid UserId { get; set; }
	public string Title { get; set; } = null!;
	public bool Completed { get; set; }

	public User User { get; set; } = new();
}
