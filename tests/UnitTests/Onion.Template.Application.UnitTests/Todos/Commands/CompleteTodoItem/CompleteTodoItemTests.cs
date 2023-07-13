using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using FluentResults;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Todos.Commands.CompleteTodoItem;
using Onion.Template.Application.Todos.Response;
using Onion.Template.Application.UnitTests.Todos.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.UnitTests.Todos.Commands.CompleteTodoItem;

public class CompleteTodoItemTests
{
	private readonly ITodoRepository _repository;
	private readonly IUserAccessor _userAccessor;
	private readonly IMapper _mapper;

	public CompleteTodoItemTests()
	{
		_repository = A.Fake<ITodoRepository>();
		_userAccessor = A.Fake<IUserAccessor>();
		_mapper = A.Fake<IMapper>();
	}

	[Fact]
	public async void Handle_TodoDoesNotExists_ReturnsResult()
	{
		// Arrange
		var command = new CompleteTodoItemCommand(Guid.NewGuid());
		Todo? todo = null;
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new CompleteTodoItemHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Should().BeOfType<Result<TodoResponse>>();
	}

	[Fact]
	public async void Handle_TodoDoesNotExists_ReturnsFailed()
	{
		// Arrange
		var command = new CompleteTodoItemCommand(Guid.NewGuid());
		Todo? todo = null;
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new CompleteTodoItemHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.IsFailed.Should().BeTrue();
	}

	[Fact]
	public async void Handle_TodoDoesNotExists_ResultContainsSingleError()
	{
		// Arrange
		var command = new CompleteTodoItemCommand(Guid.NewGuid());
		Todo? todo = null;
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new CompleteTodoItemHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Errors.Should().ContainSingle();
	}

	[Fact]
	public async void Handle_TodoExistsButCompleted_RuturnsResult()
	{
		// Arrange
		var command = new CompleteTodoItemCommand(Guid.NewGuid());
		Todo? todo = TodoFactory.CreateCompletedTask();
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new CompleteTodoItemHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Should().BeOfType<Result<TodoResponse>>();
	}

	[Fact]
	public async void Handle_TodoExistsButCompleted_ResultIsFailed()
	{
		// Arrange
		var command = new CompleteTodoItemCommand(Guid.NewGuid());
		Todo? todo = TodoFactory.CreateCompletedTask();
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new CompleteTodoItemHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.IsFailed.Should().BeTrue();
	}

	[Fact]
	public async void Handle_TodoExistsButCompleted_ResultContainsSingleError()
	{
		// Arrange
		var command = new CompleteTodoItemCommand(Guid.NewGuid());
		Todo? todo = TodoFactory.CreateCompletedTask();
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new CompleteTodoItemHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Errors.Should().ContainSingle();
	}

	[Fact]
	public async void Handle_TodoExistsButCompleted_ErrorShouldBeTaskAlreadyCompletedError()
	{
		// Arrange
		var command = new CompleteTodoItemCommand(Guid.NewGuid());
		Todo? todo = TodoFactory.CreateCompletedTask();
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new CompleteTodoItemHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Errors.First().Should().BeOfType<TaskAlreadyCompletedError>();
	}

	[Fact]
	public async void Handlde_TodoExistsNotCompleted_ShouldReturnResult()
	{
		// Arrange
		var command = new CompleteTodoItemCommand(Guid.NewGuid());
		Todo? todo = TodoFactory.CreateNewTask();
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new CompleteTodoItemHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Should().BeOfType<Result<TodoResponse>>();
	}

	[Fact]
	public async void Handlde_TodoExistsNotCompleted_ResultValueIsTodoResponse()
	{
		// Arrange
		var command = new CompleteTodoItemCommand(Guid.NewGuid());
		Todo? todo = TodoFactory.CreateNewTask();
		var response = TodoFactory.CreateTodoResponse();
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		A.CallTo(() => _mapper.Map<Todo, TodoResponse>(todo)).Returns(response);
		var handler = new CompleteTodoItemHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Value.Should().BeOfType<TodoResponse>();
	}

	[Fact]
	public async void Handlde_TodoExistsNotCompleted_ResultValueIsSimilarTo()
	{
		// Arrange
		var command = new CompleteTodoItemCommand(Guid.NewGuid());
		Todo? todo = TodoFactory.CreateNewTask();
		var response = TodoFactory.CreateTodoResponse();
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		A.CallTo(() => _mapper.Map<Todo, TodoResponse>(todo)).Returns(response);
		var handler = new CompleteTodoItemHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Value.Should().BeEquivalentTo(response);
	}
}
