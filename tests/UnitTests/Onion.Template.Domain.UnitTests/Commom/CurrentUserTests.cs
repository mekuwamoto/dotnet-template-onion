using FluentAssertions.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Onion.Template.Domain.UnitTests.Commom;

public class CurrentUserTests
{
	[Fact]
	public void CurrentUser_Constructor_ShouldBeCurrentUser()
	{
		// Arrange
		IEnumerable<Claim> claims = new List<Claim>();
		// Act
		var result = new CurrentUser(claims);
		// Assert
		result.Should().BeOfType<CurrentUser>();
	}

	[Fact]
	public void CurrentUser_ClaimsContainsNameIdentifier_ObjectShouldContainUserId()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		IEnumerable<Claim> claims = new List<Claim>() {
			new(ClaimTypes.NameIdentifier, userId.ToString())
		};
		// Act
		var result = new CurrentUser(claims);
		// Assert
		result.UserId.Should().NotBeEmpty();
		result.UserId.Should().Be(userId);
	}

	[Fact]
	public void CurrentUser_ClaimsContainsGivenName_ObjectShouldContainFirstName()
	{
		// Arrange
		string firstName = "firstName";
		IEnumerable<Claim> claims = new List<Claim>()
		{
			new (ClaimTypes.GivenName, firstName),
		};
		// Act
		var result = new CurrentUser(claims);
		// Assert
		result.FirstName.Should().NotBeEmpty();
		result.FirstName.Should().Be(firstName);
	}

	[Fact]
	public void CurrentUser_ClaimsContainsSurname_ObjectShouldContainLastName()
	{
		// Arrange
		string lastName = "lastName";
		IEnumerable<Claim> claims = new List<Claim>()
		{
			new (ClaimTypes.Surname, lastName),
		};
		// Act
		var result = new CurrentUser(claims);
		// Assert
		result.LastName.Should().NotBeEmpty();
		result.LastName.Should().Be(lastName);
	}

	[Fact]
	public void CurrentUser_ClaimsContainEmail_ObjectShouldContainEmail()
	{
		// Arrange
		string email = "firsName.lastName@email.com";
		IEnumerable<Claim> claims = new List<Claim>()
		{
			new (ClaimTypes.Email, email),
		};
		// Act
		var result = new CurrentUser(claims);
		// Assert
		result.Email.Should().NotBeEmpty();
		result.Email.Should().Be(email);
	}

	[Fact]
	public void CurrentUser_ClaimsContainExpiration_ObjectShouldContainTokenExpiration()
	{
		// Arrange
		DateTime exp = new DateTime(2023, 7, 9);
		IEnumerable<Claim> claims = new List<Claim>()
		{
			new (JwtRegisteredClaimNames.Exp, exp.ToDateTimeOffset().ToUnixTimeSeconds().ToString())
		};
		// Act
		var result = new CurrentUser(claims);
		// Assert
		result.TokenExpiration.Should().HaveDay(9);
		result.TokenExpiration.Should().HaveMonth(7);
		result.TokenExpiration.Should().HaveYear(2023);
		result.TokenExpiration.Should().Be(9.July(2023));
		result.TokenExpiration.Should().HaveHour(0);
		result.TokenExpiration.Should().HaveMinute(0);
		result.TokenExpiration.Should().HaveSecond(0);
	}

	[Fact]
	public void CurrentUser_ClaimsContainJti_ObjectShouldContainJti()
	{
		// Arrange
		Guid jti = Guid.NewGuid();
		IEnumerable<Claim> claims = new List<Claim>() {
			new(JwtRegisteredClaimNames.Jti, jti.ToString())
		};
		// Act
		var result = new CurrentUser(claims);
		// Assert
		result.Jti.Should().NotBeEmpty();
		result.Jti.Should().Be(jti);
	}

	[Fact]
	public void CurrentUser_ConstructorWithCorrectExpectedClaims_ShouldReturnCurrentUserWithAllData()
	{
		// Arrange
		Guid userId = Guid.NewGuid();
		string firstName = "firstName";
		string lastName = "lastName";
		Guid jti = Guid.NewGuid();
		string email = "firsName.lastName@email.com";
		DateTime exp = new DateTime(2023, 7, 9);
		CurrentUser user = new CurrentUser(userId, firstName, lastName, jti, email, exp);
		IEnumerable<Claim> claims = new List<Claim>()
		{
			new (ClaimTypes.NameIdentifier, userId.ToString()),
			new (ClaimTypes.GivenName, firstName),
			new (ClaimTypes.Surname, lastName),
			new (JwtRegisteredClaimNames.Jti, jti.ToString()),
			new (ClaimTypes.Email, email),
			new (JwtRegisteredClaimNames.Exp, exp.ToDateTimeOffset().ToUnixTimeSeconds().ToString())
		};
		// Act
		var result = new CurrentUser(claims);
		// Assert
		result.Should().BeOfType<CurrentUser>();
		result.Should().BeEquivalentTo(user);
	}
}

