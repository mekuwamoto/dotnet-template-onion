using FakeItEasy;
using FluentAssertions;
using FluentResults;
using Microsoft.Extensions.Options;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Commom.Settings;
using Onion.Template.Application.UnitTests.Users.Fixtures;
using Onion.Template.Application.Users.Commands.Login;
using Onion.Template.Application.Users.Requests;
using Onion.Template.Application.Users.Response.Errors;
using Onion.Template.Application.Users.Response.Successful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.UnitTests.Users.Commands.Login;

public class LoginTests
{
	private readonly IPwdHasher _hasher;
	private readonly IUserRepository _repository;
	private readonly IJwtTokenGenerator _jwt;

	public LoginTests()
	{
		_hasher = A.Fake<IPwdHasher>();
		_repository = A.Fake<IUserRepository>();
		_jwt = A.Fake<IJwtTokenGenerator>();
	}

	private LoginHandler LoginHandlerFactory()
	{
		var handler = new LoginHandler(_hasher, UsersFixtures.GenerateHashSettingsConfig(), _repository, _jwt);
		return handler;
	}

	[Fact]
	public async void LoginHandler_EmailDoesNotExistis_ReturnsResult()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var cancellationToken = new CancellationToken();
		User? user = null;
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		// Act
		var result = await handler.Handle(command, cancellationToken);
		// Assert
		result.Should().BeOfType<Result<UserTokenResponse>>();
	}

	[Fact]
	public async void LoginHandler_EmailDoesNotExistis_ReturnsResultFailed()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var cancellationToken = new CancellationToken();
		User? user = null;
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		// Act
		var result = await handler.Handle(command, cancellationToken);
		// Assert
		result.IsFailed.Should().BeTrue();
	}

	[Fact]
	public async void LoginHandler_EmailDoesNotExistis_ReturnsResultWithErrors()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var cancellationToken = new CancellationToken();
		User? user = null;
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		// Act
		var result = await handler.Handle(command, cancellationToken);
		// Assert
		result.Errors.Should().ContainSingle();
	}

	[Fact]
	public async void LoginHandler_EmailDoesNotExistis_ReturnsLoginError()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var cancellationToken = new CancellationToken();
		User? user = null;
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		// Act
		var result = await handler.Handle(command, cancellationToken);
		// Assert
		result.Errors.First().Should().BeOfType<LoginError>();
	}

	[Fact]
	public async void LoginHandler_PasswordIsWrong_ReturnsResult()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var settings = UsersFixtures.GenerateHashSettingsConfig().Value;
		var hash = Guid.NewGuid().ToString();
		User? user = UsersFixtures.GenerateGenericUser();
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		A.CallTo(() => _hasher.ComputeHash(command.User.Password, user.PasswordSalt, settings.Pepper, settings.Iterations)).Returns(hash);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Should().BeOfType<Result<UserTokenResponse>>();
	}

	[Fact]
	public async void LoginHandler_PasswordIsWrong_ShouldReturnFailed()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var settings = UsersFixtures.GenerateHashSettingsConfig().Value;
		var hash = Guid.NewGuid().ToString();
		User? user = UsersFixtures.GenerateGenericUser();
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		A.CallTo(() => _hasher.ComputeHash(command.User.Password, user.PasswordSalt, settings.Pepper, settings.Iterations)).Returns(hash);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.IsFailed.Should().BeTrue();
	}

	[Fact]
	public async void LoginHandler_PasswordIsWrong_ShouldReturnResultWithError()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var settings = UsersFixtures.GenerateHashSettingsConfig().Value;
		var hash = Guid.NewGuid().ToString();
		User? user = UsersFixtures.GenerateGenericUser();
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		A.CallTo(() => _hasher.ComputeHash(command.User.Password, user.PasswordSalt, settings.Pepper, settings.Iterations)).Returns(hash);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Errors.Should().ContainSingle();
	}

	[Fact]
	public async void LoginHandler_PasswordIsWrong_ShouldReturnLoginError()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var settings = UsersFixtures.GenerateHashSettingsConfig().Value;
		var hash = Guid.NewGuid().ToString();
		User? user = UsersFixtures.GenerateGenericUser();
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		A.CallTo(() => _hasher.ComputeHash(command.User.Password, user.PasswordSalt, settings.Pepper, settings.Iterations)).Returns(hash);
		// Act
		var result = await handler.Handle(command, new CancellationToken());
		// Assert
		result.Errors.First().Should().BeOfType<LoginError>();
	}

	[Fact]
	public async void LoginHandler_UserAndPasswordAreCorrect_ShouldReturnUserToken()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var settings = UsersFixtures.GenerateHashSettingsConfig().Value;
		User? user = UsersFixtures.GenerateGenericUser();
		UserTokenResponse token = new UserTokenResponse(Guid.NewGuid().ToString(), DateTime.Now);
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		A.CallTo(() => _hasher.ComputeHash(command.User.Password, user.PasswordSalt, settings.Pepper, settings.Iterations)).Returns(user.PasswordHash);
		A.CallTo(() => _jwt.GenerateToken(user.Id, user.FirstName, user.LastName, user.Email)).Returns(token);
		// Act
		var response = await handler.Handle(command, new CancellationToken());
		// Assert
		response.Should().BeOfType<Result<UserTokenResponse>>();
	}

	[Fact]
	public async void LoginHandler_UserAndPasswordAreCorrect_ShouldReturnResultSuccess()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var settings = UsersFixtures.GenerateHashSettingsConfig().Value;
		User? user = UsersFixtures.GenerateGenericUser();
		UserTokenResponse token = new UserTokenResponse(Guid.NewGuid().ToString(), DateTime.Now);
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		A.CallTo(() => _hasher.ComputeHash(command.User.Password, user.PasswordSalt, settings.Pepper, settings.Iterations)).Returns(user.PasswordHash);
		A.CallTo(() => _jwt.GenerateToken(user.Id, user.FirstName, user.LastName, user.Email)).Returns(token);
		// Act
		var response = await handler.Handle(command, new CancellationToken());
		// Assert
		response.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public async void LoginHandler_UserAndPasswordAreCorrect_ResultValueShouldBeUserTokenResponse()
	{
		// Arrange
		var command = new LoginCommand(UsersFixtures.GetLoginRequest());
		var handler = LoginHandlerFactory();
		var settings = UsersFixtures.GenerateHashSettingsConfig().Value;
		User? user = UsersFixtures.GenerateGenericUser();
		UserTokenResponse token = new UserTokenResponse(Guid.NewGuid().ToString(), DateTime.Now);
		A.CallTo(() => _repository.GetUserByEmail(command.User.Email)).Returns(user);
		A.CallTo(() => _hasher.ComputeHash(command.User.Password, user.PasswordSalt, settings.Pepper, settings.Iterations)).Returns(user.PasswordHash);
		A.CallTo(() => _jwt.GenerateToken(user.Id, user.FirstName, user.LastName, user.Email)).Returns(token);
		// Act
		var response = await handler.Handle(command, new CancellationToken());
		// Assert
		response.Value.Should().BeOfType<UserTokenResponse>();
	}
}
