using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using FluentResults;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Todos.Commands.RenameTodoTitle;
using Onion.Template.Application.Todos.Response;
using Onion.Template.Application.UnitTests.Todos.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.UnitTests.Todos.Commands.RenameTodoTitle;

public class RenameTodoTitleTests
{
	private readonly ITodoRepository _repository;
	private readonly IUserAccessor _userAccessor;
	private readonly IMapper _mapper;

	public RenameTodoTitleTests()
	{
		_repository = A.Fake<ITodoRepository>();
		_userAccessor = A.Fake<IUserAccessor>();
		_mapper = A.Fake<IMapper>();
	}

	[Fact]
	public async void Handler_TodoNotExists_ReturnResult()
	{
		// Arrange
		Todo? todo = null;
		var command = TodoFactory.CreateRenameTodoTitleCommand();
		var userId = Guid.NewGuid();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new RenameTodoTitleHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Should().BeOfType<Result<TodoResponse>>();
	}

	[Fact]
	public async void Handler_TodoNotExists_ResultIsFailed()
	{
		// Arrange
		Todo? todo = null;
		var command = TodoFactory.CreateRenameTodoTitleCommand();
		var userId = Guid.NewGuid();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new RenameTodoTitleHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.IsFailed.Should().BeTrue();
	}

	[Fact]
	public async void Handler_TodoNotExists_ResultContainsSingleError()
	{
		// Arrange
		Todo? todo = null;
		var command = TodoFactory.CreateRenameTodoTitleCommand();
		var userId = Guid.NewGuid();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new RenameTodoTitleHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Errors.Should().ContainSingle();
	}

	[Fact]
	public async void Handler_TodoNotExists_ResultIsNotFoundError()
	{
		// Arrange
		Todo? todo = null;
		var command = TodoFactory.CreateRenameTodoTitleCommand();
		var userId = Guid.NewGuid();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new RenameTodoTitleHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Errors.First().Should().BeOfType<NotFoundTodoError>();
	}

	[Fact]
	public async void Handler_Exists_ShoundReturnResult()
	{
		// Arrange
		Todo? todo = TodoFactory.CreateCompletedTask();
		var command = TodoFactory.CreateRenameTodoTitleCommand();
		var userId = Guid.NewGuid();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new RenameTodoTitleHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Should().BeOfType<Result<TodoResponse>>();
	}

	[Fact]
	public async void Handler_Exists_ReturnIsCompleted()
	{
		// Arrange
		Todo? todo = TodoFactory.CreateCompletedTask();
		var command = TodoFactory.CreateRenameTodoTitleCommand();
		var userId = Guid.NewGuid();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		var handler = new RenameTodoTitleHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public async void Handler_Exists_ReturnIsTodoResponse()
	{
		// Arrange
		Todo? todo = TodoFactory.CreateCompletedTask();
		var command = TodoFactory.CreateRenameTodoTitleCommand();
		var userId = Guid.NewGuid();
		var expected = TodoFactory.CreateCompletedTask();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		A.CallTo(() => _mapper.Map<Todo?, TodoResponse>(todo)).ReturnsLazily((Todo todo) => TodoFactory.CreateTodoResponse());
		var handler = new RenameTodoTitleHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Value.Should().BeOfType<TodoResponse>();
	}

	[Fact]
	public async void Handler_Exists_ReturnIsEquivalent()
	{
		// Arrange
		Todo? todo = TodoFactory.CreateCompletedTask();
		var command = TodoFactory.CreateRenameTodoTitleCommand();
		var userId = Guid.NewGuid();
		var expected = TodoFactory.CreateTodoResponse();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, command.TodoId)).Returns(todo);
		A.CallTo(() => _mapper.Map<Todo?, TodoResponse>(todo)).ReturnsLazily((Todo todo) => TodoFactory.CreateTodoResponse());
		var handler = new RenameTodoTitleHandler(_repository, _userAccessor, _mapper);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Value.Should().BeEquivalentTo(expected, options => options.Excluding(res => res.TodoId));
	}
}
