using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
