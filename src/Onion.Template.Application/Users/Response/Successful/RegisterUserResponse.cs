using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Users.Response.Successful;

public class RegisterUserResponse
{
	public Guid Id { get; set; }
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Username { get; set; } = null!;
}
