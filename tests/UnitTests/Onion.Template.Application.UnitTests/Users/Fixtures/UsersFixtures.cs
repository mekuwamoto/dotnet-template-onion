using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Onion.Template.Application.Commom.Settings;
using Onion.Template.Application.Users.Commands.Register;
using Onion.Template.Application.Users.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.UnitTests.Users.Fixtures;

public static class UsersFixtures
{
	public static LoginRequest GetLoginRequest()
	{
		var loginRequest = new LoginRequest()
		{
			Email = "email@email.com",
			Password = "pwd123"
		};
		return loginRequest;
	}

	public static IOptions<HashSettings> GenerateHashSettingsConfig()
	{
		IOptions<HashSettings> config = Options.Create(new HashSettings()
		{
			Iterations = 5,
			Pepper = "77f234c8-618e-4028-9aa1-b4851c60d6c8"
		});
		return config;
	}

	public static User GenerateGenericUser()
	{
		return new User(
			"firstName",
			"lastName",
			"firsName.lastName@email.com",
			"first",
			"YWxcmfS4GWG2U3Ai8WkOcg==",
			"cZdnYHY79cCBQzPoSvYChUMcU8iGUYh6rC2C1/PbdWk="
		);
	}

	public static RegisterUserRequest GetRegisterRequest()
	{
		return new RegisterUserRequest()
		{
			FirstName = "firstName",
			LastName = "lastName",
			Email = "firstName.lastName@email.com",
			Username = "first",
			Password = "password123",
		};
	}

}
