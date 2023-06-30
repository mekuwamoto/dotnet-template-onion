using System.ComponentModel.DataAnnotations;

namespace Onion.Template.Application.Users.Requests;

public class RegisterUserRequest
{
    [Required]
    public string FirstName { get; set; } = null!;
	[Required]
	public string LastName { get; set; } = null!;
	[Required, EmailAddress]
	public string Email { get; set; } = null!;
	[Required]
	public string Username { get; set; } = null!;
	[Required]
	public string Password { get; set; } = null!;
}
