using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings;

        public JwtTokenGenerator(IOptions<JwtSettings> settings)
            => _settings = settings.Value;

        public string GenerateToken(Guid userId, string firstName, string lastName)
        {

            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)), SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _settings.Issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_settings.ExpiryMinutes),
                signingCredentials: signingCredentials
            );
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}
