using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.User.Commands.Register;

public class RegisterCommand : IRequest<string>
{
}

public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return "ok";
    }
}
