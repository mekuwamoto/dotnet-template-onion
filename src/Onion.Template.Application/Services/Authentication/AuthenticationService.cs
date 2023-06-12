using Onion.Template.Application.Commom.Attributes;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Services.Authentication;

[Dependency(DI.Transient)]
internal class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwt;

    public AuthenticationService(IJwtTokenGenerator jwt)
    {
        _jwt = jwt;
    }

    public string Register(string firstName, string lastName, string password, string email)
    {
        Guid userId = new Guid();
        string token = _jwt.GenerateToken(userId, firstName, lastName);
        return token;
        throw new NotImplementedException();
    }
}
