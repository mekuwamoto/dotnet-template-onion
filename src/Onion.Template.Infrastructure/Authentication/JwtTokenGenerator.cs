using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Users.Response.Successful;
using Onion.Template.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Onion.Template.Infrastructure.Authentication;

[Injection(DI.Scoped)]
public class JwtTokenGenerator : IJwtTokenGenerator
{
	private readonly JwtSettings _settings;

	public JwtTokenGenerator(IOptions<JwtSettings> settings)
		=> _settings = settings.Value;

	public UserTokenResponse GenerateToken(Guid userId, string firstName, string lastName, string email)
	{
		SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)), SecurityAlgorithms.HmacSha256);
		Claim[] claims = GenerateClaims(userId, firstName, lastName, email);
		DateTime expiration = DateTime.Now.AddMinutes(_settings.ExpiryMinutes);
		JwtSecurityToken securityToken = new JwtSecurityToken(
			issuer: _settings.Issuer,
			claims: claims,
			expires: expiration,
			signingCredentials: signingCredentials
		);
		var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
		string token = jwtSecurityTokenHandler.WriteToken(securityToken);
		return new UserTokenResponse(token, expiration);
	}

	private static Claim[] GenerateClaims(Guid userId, string firstName, string lastName, string email)
	{
		return new[]
		{
			new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
			new Claim(ClaimTypes.GivenName, firstName),
			new Claim(ClaimTypes.Surname, lastName),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.Email, email)
		};
	}
}
