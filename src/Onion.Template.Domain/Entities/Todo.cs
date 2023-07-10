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

	public Guid UserId { get; private set; }
	public string Title { get; private set; } = null!;
	public bool Completed { get; private set; } = false;
	public DateTime DtIncluded { get; private set; } = DateTime.UtcNow;
	public DateTime DtLastModified { get; private set; } = DateTime.UtcNow;
	public DateTime? DtExcluded { get; private set; } = null;

	public User User { get; set; } = null!;

	public void CompleteTask()
	{
		Completed = true;
		DtLastModified = DateTime.UtcNow;
	}

	public void RenameTitle(string newTitle)
	{
		Title = newTitle;
		DtLastModified = DateTime.UtcNow;
	}
}
