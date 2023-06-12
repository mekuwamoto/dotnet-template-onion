using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Commom.Interfaces.Authentication;

public interface IAuthenticationService
{
    string Register(string firstName, string lastName, string password, string email);
}
