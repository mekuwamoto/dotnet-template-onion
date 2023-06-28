using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Domain.Entities;

public class User
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string FirstName { get; private set; } = null!;
	public string LastName { get; private set; } = null!;
	public string Email { get; private set; } = null!;
	public string Username { get; private set; } = null!;
	public string PasswordSalt { get; private set; } = null!;
	public string PasswordHash { get; private set; } = null!;

	public User(string firstName, string lastName, string email, string username, string passwordSalt, string passwordHash)
	{
		FirstName = firstName;
		LastName = lastName;
		Email = email;
		Username = username;
		PasswordSalt = passwordSalt;
		PasswordHash = passwordHash;
	}

	public User()
	{
	}
}
