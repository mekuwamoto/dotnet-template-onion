namespace Onion.Template.Application.Users.Response.Successful;

public class UserTokenResponse
{
	public string Token { get; set; } = null!;
	public DateTime Expiration { get; set; }
	public string TokenType => "Bearer";

	public UserTokenResponse(string token, DateTime expiration)
	{
		Token = token;
		Expiration = expiration;
	}
}
