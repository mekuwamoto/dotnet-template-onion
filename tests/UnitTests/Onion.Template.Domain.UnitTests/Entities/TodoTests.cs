using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using Onion.Template.Domain.Entities;

namespace Onion.Template.Domain.UnitTests.Entities;

public class TodoTests
{
	[Fact]
	public void Todo_Constructor_ShouldReturnTodo()
	{
		// Arrange
		string title = "title";
		Guid userId = Guid.NewGuid();
		// Act
		var result = new Todo(title, userId);
		// Assert
		result.Should().BeOfType<Todo>();
	}

	[Fact]
	public void Todo_Constructor_ShouldReturnDefaultValues()
	{
		// Arrange
		string title = "title";
		Guid userId = Guid.NewGuid();
		DateTime now = DateTime.UtcNow;
		// Act
		var result = new Todo(title, userId);
		// Assert
		result.Id.Should().NotBeEmpty();
		result.Completed.Should().BeFalse();
		result.DtIncluded.Should().BeOnOrAfter(now);
		result.DtIncluded.Should().BeOnOrBefore(result.DtLastModified);
		result.DtLastModified.Should().BeOnOrAfter(now);
		result.DtIncluded.Should().BeCloseTo(now, 1.Seconds());
		result.User.Should().BeNull();
	}

	[Fact]
	public void Todo_CompleteTask_ShouldChangeStatusAndLastModifiedDate()
	{
		// Arrange
		string title = "title";
		Guid userId = Guid.NewGuid();
		var todo = new Todo(title, userId);
		Thread.Sleep(1500);
		DateTime now = DateTime.UtcNow;
		// Act
		todo.CompleteTask();

		// Assert
		todo.Completed.Should().BeTrue();
		todo.DtLastModified.Should().BeAfter(todo.DtIncluded);
		todo.DtLastModified.Should().BeCloseTo(now, 1.Seconds());
	}

	[Fact]
	public void Todo_RenameTitle_ShouldChangeTitleAndLastModifiedDate()
	{
		// Arrange
		string title = "title";
		string newTitle = "newTitle";
		Guid userId = Guid.NewGuid();
		var todo = new Todo(title, userId);
		Thread.Sleep(1500);
		DateTime now = DateTime.UtcNow;
		// Act
		todo.RenameTitle(newTitle);
		// Assert
		todo.Title.Should().Be(newTitle);
		todo.Title.Should().NotBe(title);
		todo.DtLastModified.Should().BeAfter(todo.DtIncluded);
		todo.DtLastModified.Should().BeCloseTo(now, 1.Seconds());
	}

}
