using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Domain.Entities;

public class Todo : BaseEntity
{
	public Todo(string title, Guid userId)
	{
		Title = title;
		UserId = userId;
	}

	public Guid UserId { get; set; }
	public string Title { get; set; } = null!;
	public bool Completed { get; private set; } = false;
	public DateTime DtIncluded { get; private set; } = DateTime.UtcNow;
	public DateTime DtLastModified { get; private set; } = DateTime.UtcNow;
	public DateTime? DtExluded { get; private set; } = null;

	public User User { get; set; } = null!;

	public void CompleteTask()
	{
		Completed = true;
	}

	public void RenameTitle(string newTitle)
	{
		Title = newTitle;
	}
}
