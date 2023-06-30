using Onion.Template.Application.Users.Response.Successful;

namespace Onion.Template.Application.Commom.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
	UserTokenResponse GenerateToken(Guid userId, string firstName, string lastName, string email);
}
