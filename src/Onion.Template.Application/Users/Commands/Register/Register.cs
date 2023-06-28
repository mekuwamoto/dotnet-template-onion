using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Commom.Settings;
using Onion.Template.Application.Users.Requests;
using Onion.Template.Application.Users.Response;
using Onion.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Users.Commands.Register;

public struct RegisterCommand : IRequest<RegisterUserResponse>
{
	public RegisterCommand(RegisterUserRequest user) => User = user;
	public RegisterUserRequest User { get; set; }
}

public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterUserResponse>
{
	private readonly IPwdHasher _hasher;
	private readonly HashSettings _settings;
	private readonly IMapper _mapper;

	public RegisterHandler(IPwdHasher wdHasher, IOptions<HashSettings> settings, IMapper mapper)
	{
		_hasher = wdHasher;
		_settings = settings.Value;
		_mapper = mapper;
	}

	public async Task<RegisterUserResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
	{
		string salt = _hasher.GenerateSalt();
		string hash = _hasher.ComputeHash(command.User.Password, salt, _settings.Pepper, _settings.Iterations);

		User user = new User(command.User.FirstName, command.User.LastName, command.User.Email, command.User.Username, salt, hash);

		RegisterUserResponse userResponse = _mapper.Map<RegisterUserResponse>(user);
		return userResponse;
	}
}