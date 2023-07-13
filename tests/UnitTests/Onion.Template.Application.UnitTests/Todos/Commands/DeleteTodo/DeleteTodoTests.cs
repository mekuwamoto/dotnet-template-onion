using FakeItEasy;
using FluentAssertions;
using FluentResults;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Todos.Commands.DeleteTodo;
using Onion.Template.Application.Todos.Response;
using Onion.Template.Application.UnitTests.Todos.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.UnitTests.Todos.Commands.DeleteTodo;

public class DeleteTodoTests
{
	private readonly ITodoRepository _repository;
	private readonly IUserAccessor _userAccessor;

	public DeleteTodoTests()
	{
		_repository = A.Fake<ITodoRepository>();
		_userAccessor = A.Fake<IUserAccessor>();
	}

	[Fact]
	public async void Handle_TodoDoesNotExists_ReturnResult()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		Todo? todo = null;
		var request = TodoFactory.DeleteTodoCommand();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, request.TodoId)).Returns(todo);
		var handler = new DeleteTodoHandler(_repository, _userAccessor);
		// Act
		var result = await handler.Handle(request, new CancellationToken());
		// Assert
		result.Should().BeOfType<Result>();
	}

	[Fact]
	public async void Handle_TodoDoesNotExists_ReturnFailed()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		Todo? todo = null;
		var request = TodoFactory.DeleteTodoCommand();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, request.TodoId)).Returns(todo);
		var handler = new DeleteTodoHandler(_repository, _userAccessor);
		// Act
		var result = await handler.Handle(request, new CancellationToken());
		// Assert
		result.IsFailed.Should().BeTrue();
	}

	[Fact]
	public async void Handle_TodoDoesNotExists_ResultContainsOneError()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		Todo? todo = null;
		var request = TodoFactory.DeleteTodoCommand();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, request.TodoId)).Returns(todo);
		var handler = new DeleteTodoHandler(_repository, _userAccessor);
		// Act
		var result = await handler.Handle(request, new CancellationToken());
		// Assert
		result.Errors.Should().ContainSingle();
	}

	[Fact]
	public async void Handle_TodoDoesNotExists_ResultErrorIsNotFoundTodoError()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		Todo? todo = null;
		var request = TodoFactory.DeleteTodoCommand();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, request.TodoId)).Returns(todo);
		var handler = new DeleteTodoHandler(_repository, _userAccessor);
		// Act
		var result = await handler.Handle(request, new CancellationToken());
		// Assert
		result.Errors.First().Should().BeOfType<NotFoundTodoError>();
	}

	[Fact]
	public async void Handle_TodoExists_ResultResult()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		Todo? todo = TodoFactory.CreateNewTask();
		var request = TodoFactory.DeleteTodoCommand();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, request.TodoId)).Returns(todo);
		var handler = new DeleteTodoHandler(_repository, _userAccessor);
		// Act
		var result = await handler.Handle(request, new CancellationToken());
		// Assert
		result.Should().BeOfType<Result>();
	}

	[Fact]
	public async void Handle_TodoExists_ResultIsSuccess()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		Todo? todo = TodoFactory.CreateNewTask();
		var request = TodoFactory.DeleteTodoCommand();
		A.CallTo(() => _userAccessor.UserId).Returns(userId);
		A.CallTo(() => _repository.GetTodoFromUser(_userAccessor.UserId, request.TodoId)).Returns(todo);
		var handler = new DeleteTodoHandler(_repository, _userAccessor);
		// Act
		var result = await handler.Handle(request, new CancellationToken());
		// Assert
		result.IsSuccess.Should().BeTrue();	
	}
}
