using MediatR;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Users.Commands.Register;

public class RegisterCommand : IRequest<string>
{
}

public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthenticationService _authService;

    public RegisterHandler(IAuthenticationService authService)
    {
        _authService = authService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var token = _authService.Register("Marcelo", "Eiji", "email@email.com");
        return token;
    }
}
