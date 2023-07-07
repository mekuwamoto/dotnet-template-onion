using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Domain.Commom;

public class CurrentUser
{
	public Guid UserId { get; private set; }
	public string FirstName { get; private set; } = null!;
	public string LastName { get; private set; } = null!;
	public Guid Jti { get; private set; }
	public string Email { get; private set; } = null!;
	public DateTime TokenExpiration { get; set; }

	public CurrentUser(IEnumerable<Claim> claims)
	{
		foreach (var claim in claims)
		{
			switch (claim.Type)
			{
				case ClaimTypes.NameIdentifier:
					UserId = Guid.Parse(claim.Value);
					break;
				case ClaimTypes.GivenName:
					FirstName = claim.Value;
					break;
				case ClaimTypes.Surname:
					LastName = claim.Value;
					break;
				case JwtRegisteredClaimNames.Jti:
					Jti = Guid.Parse(claim.Value);
					break;
				case ClaimTypes.Email:
					Email = claim.Value;
					break;
				case JwtRegisteredClaimNames.Exp:
					TokenExpiration = DateTime.UnixEpoch.AddSeconds(Convert.ToDouble(claim.Value));
					break;
			}
		}
	}
}
