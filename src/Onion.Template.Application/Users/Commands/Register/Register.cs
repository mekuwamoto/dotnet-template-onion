using FluentResults;
using MediatR;
using Microsoft.Extensions.Options;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Commom.Settings;
using Onion.Template.Application.Users.Requests;
using Onion.Template.Application.Users.Response.Errors;
using Onion.Template.Application.Users.Response.Successful;
using Onion.Template.Domain.Entities;

namespace Onion.Template.Application.Users.Commands.Register;

public struct RegisterCommand : IRequest<Result<UserTokenResponse>>
{
	public RegisterCommand(RegisterUserRequest user) => User = user;
	public RegisterUserRequest User { get; set; }
}

public class RegisterHandler : IRequestHandler<RegisterCommand, Result<UserTokenResponse>>
{
	private readonly IPwdHasher _hasher;
	private readonly HashSettings _settings;
	private readonly IUserRepository _repository;
	private readonly IJwtTokenGenerator _jwt;

	public RegisterHandler(IPwdHasher wdHasher, IOptions<HashSettings> settings, IUserRepository repository, IJwtTokenGenerator jwt)
	{
		_hasher = wdHasher;
		_settings = settings.Value;
		_repository = repository;
		_jwt = jwt;
	}

	public async Task<Result<UserTokenResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
	{
		if (await _repository.IsEmailRegistered(command.User.Email))
			return Result.Fail<UserTokenResponse>(new DuplicateEmailError());

		string salt = _hasher.GenerateSalt();
		string hash = _hasher.ComputeHash(command.User.Password, salt, _settings.Pepper, _settings.Iterations);

		User user = new User(command.User.FirstName, command.User.LastName, command.User.Email, command.User.Username, salt, hash);

		User createdUser = await _repository.AddAsync(user);

		UserTokenResponse token = _jwt.GenerateToken(createdUser.Id, createdUser.FirstName, createdUser.LastName, createdUser.Email);
		return token;
	}
}