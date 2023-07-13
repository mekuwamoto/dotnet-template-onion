using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Todos.Commands.CreateTodo;
using Onion.Template.Application.Todos.Requests;
using Onion.Template.Application.Todos.Response;
using Onion.Template.Application.UnitTests.Todos.Factories;
using Onion.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.UnitTests.Todos.Commands.CreateTodo;

public class CreateTodoCommand
{
	private readonly ITodoRepository _repository;
	private readonly IUserAccessor _accessor;
	private readonly IMapper _mapper;

	public CreateTodoCommand()
	{
		_repository = A.Fake<ITodoRepository>();
		_accessor = A.Fake<IUserAccessor>();
		_mapper = A.Fake<IMapper>();
	}

	[Fact]
	public async void Handle_ValidCommand_ReturnsCreatedTodoResponse()
	{
		// Arrange
		var userId = Guid.NewGuid();
		var command = TodoFactory.CreateTodoCommand();
		var todo = TodoFactory.CreateNewTask();
		var createdTodo = TodoFactory.CreateNewTask();
		A.CallTo(() => _accessor.UserId).Returns(todo.UserId);
		A.CallTo(() => _repository.AddAsync(todo)).ReturnsLazily(() => todo);
		A.CallTo(() => _repository.AddAsync(A<Todo>._)).Returns(Task.FromResult(TodoFactory.CreateNewTask()));
		A.CallTo(() => _mapper.Map<Todo, TodoResponse>(createdTodo)).Returns(TodoFactory.CreateTodoResponse());
		A.CallTo(() => _mapper.Map<Todo, TodoResponse>(A<Todo>._)).ReturnsLazily((Todo todo) => TodoFactory.CreateTodoResponse());
		var handler = new CreateTodoHandler(_repository, _accessor, _mapper);
		// Act
		var response = await handler.Handle(command, new CancellationToken());
		// Assert
		response.Should().BeOfType<TodoResponse>();
	}

	[Fact]
	public async void Handle_ValidCommand_ReturnsTodoResponseWithSameProperties()
	{
		// Arrange
		var userId = Guid.NewGuid();
		var command = TodoFactory.CreateTodoCommand();
		var todo = TodoFactory.CreateNewTask();
		var createdTodo = TodoFactory.CreateNewTask();
		var expectedResponse = TodoFactory.CreateTodoResponse();
		A.CallTo(() => _accessor.UserId).Returns(todo.UserId);
		A.CallTo(() => _repository.AddAsync(todo)).ReturnsLazily(() => todo);
		A.CallTo(() => _repository.AddAsync(A<Todo>._)).Returns(Task.FromResult(TodoFactory.CreateNewTask()));
		A.CallTo(() => _mapper.Map<Todo, TodoResponse>(createdTodo)).Returns(TodoFactory.CreateTodoResponse());
		A.CallTo(() => _mapper.Map<Todo, TodoResponse>(A<Todo>._)).ReturnsLazily((Todo todo) => TodoFactory.CreateTodoResponse());
		var handler = new CreateTodoHandler(_repository, _accessor, _mapper);
		// Act
		var response = await handler.Handle(command, new CancellationToken());
		// Assert
		response.Should().BeEquivalentTo(expectedResponse, op => op.Excluding(res => res.TodoId));
	}
}
