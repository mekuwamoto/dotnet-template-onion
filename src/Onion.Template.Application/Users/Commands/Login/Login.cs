using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Application.Commom.Settings;
using Onion.Template.Application.Users.Requests;
using Onion.Template.Application.Users.Response;
using Onion.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Users.Commands.Login;

public struct LoginCommand : IRequest<LoginUserResponse>
{
	public LoginRequest User { get; set; }
	public LoginCommand(LoginRequest user) => User = user;

}

public class LoginHandler : IRequestHandler<LoginCommand, LoginUserResponse>
{
	private readonly IPwdHasher _hasher;
	private readonly HashSettings _settings;
	private readonly IUserRepository _repository;
	private readonly IMapper _mapper;


	public LoginHandler(IPwdHasher hasher, IOptions<HashSettings> settings, IUserRepository repository, IMapper mapper)
	{
		_hasher = hasher;
		_settings = settings.Value;
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<LoginUserResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
	{
		User? user = await _repository.GetUserByEmail(command.User.Email);

		if (user is null)
			throw new Exception("Email or password did not match");

		string passwordHash = _hasher.ComputeHash(
			command.User.Password,
			user.PasswordSalt,
			_settings.Pepper,
			_settings.Iterations
		);

		if (user.PasswordHash != passwordHash)
			throw new Exception("Email or password did not match");

		LoginUserResponse userResponse = _mapper.Map<LoginUserResponse>(user);
		return userResponse;
	}
}