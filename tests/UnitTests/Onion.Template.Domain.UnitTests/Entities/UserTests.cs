using FluentAssertions;
using Onion.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Domain.UnitTests.Entities;

public class UserTests
{
	[Fact]
	public void User_Constructor_ShouldReturnUser()
	{
		// Arrange
		string firstName = "firstName";
		string lastName = "lastName";
		string email = "email";
		string username = "usernam";
		string passwordSalt = "passwordSalt";
		string passwordHash = "passwordHash";
		// Act
		var result = new User(firstName, lastName, email, username, passwordSalt, passwordHash);
		// Assert
		result.Should().BeOfType<User>();
	}

	[Fact]
	public void User_Constructor_ShouldGenerateNewGuid()
	{
		// Act
		var result = new User("", "", "", "", "", "");
		// Assert
		result.Id.Should().NotBeEmpty();
	}

	[Fact]
	public void User_Constructor_ShouldHavePropertiesFromConstructorInputs()
	{
		// Arrange
		string firstName = "firstName";
		string lastName = "lastName";
		string email = "email";
		string username = "usernam";
		string passwordSalt = "passwordSalt";
		string passwordHash = "passwordHash";
		// Act
		var result = new User(firstName, lastName, email, username, passwordSalt, passwordHash);
		// Assert
		result.FirstName.Should().Be(firstName);
		result.LastName.Should().Be(lastName);
		result.Email.Should().Be(email);
		result.Username.Should().Be(username);
		result.PasswordSalt.Should().Be(passwordSalt);
		result.PasswordHash .Should().Be(passwordHash);
	}
}
