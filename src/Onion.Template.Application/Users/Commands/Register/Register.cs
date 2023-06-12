using MediatR;
using Onion.Template.Application.Commom.Interfaces.Authentication;
using Onion.Template.Application.Users.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Users.Commands.Register;

public struct RegisterCommand : IRequest<string>
{
    public RegisterCommand(RegisterUserRequest user) => User = user;
    public RegisterUserRequest User { get; set; }
}

public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthenticationService _authService;

    public RegisterHandler(IAuthenticationService authService)
    {
        _authService = authService;
    }

    public async Task<string> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var token = _authService.Register("firstName", "lastName", "password", "email");
        return token;
    }
}