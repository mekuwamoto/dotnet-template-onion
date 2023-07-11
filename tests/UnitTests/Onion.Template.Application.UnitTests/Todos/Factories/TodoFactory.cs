﻿using Onion.Template.Application.Todos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.UnitTests.Todos.Factories;

public static class TodoFactory
{
	private static string DefaultTitle => "Task todo";
	private static Guid DefaultGuid => new Guid("2da2d889-fa7b-e8b3-4302-ac37153a1bb4");

	public static Todo CreateNewTask()
		=> new Todo(DefaultTitle, DefaultGuid);

	public static Todo CreateCompletedTask()
	{
		var todo = CreateNewTask();
		todo.CompleteTask();
		return todo;
	}

	public static TodoResponse CreateTodoResponse()
	{
		var todo = CreateCompletedTask();
		return new TodoResponse
		{
			TodoId = todo.Id,
			Title = todo.Title,
			Completed = todo.Completed
		};
	}
}
