using Onion.Template.Application.Commom.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Services.Authentication;

internal class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwt;

    public AuthenticationService(IJwtTokenGenerator jwt)
    {
        _jwt = jwt;
    }

    public string Register(string firstName, string lastName, string email)
    {
        //Create jwt
        Guid userId = new Guid();
        string token = _jwt.GenerateToken(userId, firstName, lastName);
        return token;
    }
}
