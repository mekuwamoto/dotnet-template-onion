﻿using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Infrastructure.Authentication;

[Injection(DI.Scoped)]
public class PwdHasher : IPwdHasher
{
	public string ComputeHash(string password, string salt, string pepper, int iteration)
	{
		if (iteration <= 0) return password;

		using var sha256 = SHA256.Create();
		var passwordSaltPepper = $"{password}{salt}{pepper}";
		var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
		var byteHash = sha256.ComputeHash(byteValue);
		var hash = Convert.ToBase64String(byteHash);
		return ComputeHash(hash, salt, pepper, iteration - 1);
	}

	public string GenerateSalt()
	{
		using var rng = RandomNumberGenerator.Create();
		var byteSalt = new byte[16];
		rng.GetBytes(byteSalt);
		var salt = Convert.ToBase64String(byteSalt);
		return salt;
	}
}
