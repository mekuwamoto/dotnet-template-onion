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

namespace Onion.Template.Application.Users.Commands.Login;

public struct LoginCommand : IRequest<Result<UserTokenResponse>>
{
	public LoginRequest User { get; set; }
	public LoginCommand(LoginRequest user) => User = user;

}

public class LoginHandler : IRequestHandler<LoginCommand, Result<UserTokenResponse>>
{
	private readonly IPwdHasher _hasher;
	private readonly HashSettings _settings;
	private readonly IUserRepository _repository;
	private readonly IJwtTokenGenerator _jwt;

	public LoginHandler(IPwdHasher hasher, IOptions<HashSettings> settings, IUserRepository repository, IJwtTokenGenerator jwt)
	{
		_hasher = hasher;
		_settings = settings.Value;
		_repository = repository;
		_jwt = jwt;
	}

	public async Task<Result<UserTokenResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
	{
		User? user = await _repository.GetUserByEmail(command.User.Email);

		if (user is null)
			return Result.Fail<UserTokenResponse>(new LoginError());

		string passwordHash = _hasher.ComputeHash(
			command.User.Password,
			user.PasswordSalt,
			_settings.Pepper,
			_settings.Iterations
		);

		if (user.PasswordHash != passwordHash)
			return Result.Fail<UserTokenResponse>(new LoginError());

		UserTokenResponse token = _jwt.GenerateToken(user.Id, user.FirstName, user.LastName, user.Email);
		return token;
	}
}