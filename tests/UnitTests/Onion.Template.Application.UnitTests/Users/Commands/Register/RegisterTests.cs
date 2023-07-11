using FakeItEasy;
using FluentAssertions;
using FluentResults;
using Microsoft.Extensions.Options;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Commom.Settings;
using Onion.Template.Application.UnitTests.Users.Fixtures;
using Onion.Template.Application.Users.Commands.Register;
using Onion.Template.Application.Users.Response.Errors;
using Onion.Template.Application.Users.Response.Successful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.UnitTests.Users.Commands.Register;

public class RegisterTests
{
	private readonly IPwdHasher _hasher;
	private readonly IOptions<HashSettings> _settings;
	private readonly IUserRepository _repository;
	private readonly IJwtTokenGenerator _jwt;

	public RegisterTests()
	{
		_hasher = A.Fake<IPwdHasher>();
		_repository = A.Fake<IUserRepository>();
		_jwt = A.Fake<IJwtTokenGenerator>();
		_settings = UsersFixtures.GenerateHashSettingsConfig();
	}

	[Fact]
	public async void RegisterHandler_RegisterAlreadyRegistered_ShouldReturnResult()
	{
		// Arrange
		var request = new RegisterCommand(UsersFixtures.GetRegisterRequest());
		var handler = new RegisterHandler(_hasher, _settings, _repository, _jwt);
		A.CallTo(() => _repository.IsEmailRegistered(request.User.Email)).Returns(true);
		// Act
		var result = await handler.Handle(request, new CancellationToken());
		// Assert
		result.Should().BeOfType<Result<UserTokenResponse>>();
	}

	[Fact]
	public async void RegisterHandler_RegisterAlreadyRegistered_ShouldReturnFail()
	{
		// Arrange
		var request = new RegisterCommand(UsersFixtures.GetRegisterRequest());
		var handler = new RegisterHandler(_hasher, _settings, _repository, _jwt);
		A.CallTo(() => _repository.IsEmailRegistered(request.User.Email)).Returns(true);
		// Act
		var result = await handler.Handle(request, new CancellationToken());
		// Assert
		result.IsFailed.Should().BeTrue();
	}

	[Fact]
	public async void RegisterHandler_RegisterAlreadyRegistered_ShouldContainSingleError()
	{
		// Arrange
		var request = new RegisterCommand(UsersFixtures.GetRegisterRequest());
		var handler = new RegisterHandler(_hasher, _settings, _repository, _jwt);
		A.CallTo(() => _repository.IsEmailRegistered(request.User.Email)).Returns(true);
		// Act
		var result = await handler.Handle(request, new CancellationToken());
		// Assert
		result.Errors.Should().ContainSingle();
	}

	[Fact]
	public async void RegisterHandler_RegisterAlreadyRegistered_ErrorShouldBeDuplicateError()
	{
		// Arrange
		var request = new RegisterCommand(UsersFixtures.GetRegisterRequest());
		var handler = new RegisterHandler(_hasher, _settings, _repository, _jwt);
		A.CallTo(() => _repository.IsEmailRegistered(request.User.Email)).Returns(true);
		// Act
		var result = await handler.Handle(request, new CancellationToken());
		// Assert
		result.Errors.First().Should().BeOfType<DuplicateEmailError>();
	}

	[Fact]
	public async void RegisterHandler_RegisterNewUser_ShouldReturnResult()
	{
		// Arrange
		var request = new RegisterCommand(UsersFixtures.GetRegisterRequest());
		var handler = new RegisterHandler(_hasher, _settings, _repository, _jwt);
		var user = UsersFixtures.GenerateGenericUser();
		var userTokenResponse = new UserTokenResponse(Guid.NewGuid().ToString(), DateTime.Now);
		A.CallTo(() => _repository.IsEmailRegistered(request.User.Email)).Returns(false);
		A.CallTo(() => _hasher.GenerateSalt()).Returns(user.PasswordSalt);
		A.CallTo(() => _hasher.ComputeHash(request.User.Password, user.PasswordSalt, _settings.Value.Pepper, _settings.Value.Iterations)).Returns(user.PasswordHash);
		A.CallTo(() => _repository.AddAsync(user)).Returns(user);
		A.CallTo(() => _jwt.GenerateToken(user.Id, user.FirstName, user.LastName, user.Email)).Returns(userTokenResponse);
		// Act
		var response = await handler.Handle(request, new CancellationToken());
		// Assert
		response.Should().BeOfType<Result<UserTokenResponse>>();
	}

	[Fact]
	public async void RegisterHandler_RegisterNewUser_ShouldReturnSuccess()
	{
		// Arrange
		var request = new RegisterCommand(UsersFixtures.GetRegisterRequest());
		var handler = new RegisterHandler(_hasher, _settings, _repository, _jwt);
		var user = UsersFixtures.GenerateGenericUser();
		var userTokenResponse = new UserTokenResponse(Guid.NewGuid().ToString(), DateTime.Now);
		A.CallTo(() => _repository.IsEmailRegistered(request.User.Email)).Returns(false);
		A.CallTo(() => _hasher.GenerateSalt()).Returns(user.PasswordSalt);
		A.CallTo(() => _hasher.ComputeHash(request.User.Password, user.PasswordSalt, _settings.Value.Pepper, _settings.Value.Iterations)).Returns(user.PasswordHash);
		A.CallTo(() => _repository.AddAsync(user)).Returns(user);
		A.CallTo(() => _jwt.GenerateToken(user.Id, user.FirstName, user.LastName, user.Email)).Returns(userTokenResponse);
		// Act
		var response = await handler.Handle(request, new CancellationToken());
		// Assert
		response.IsSuccess.Should().BeTrue();
	}
}
