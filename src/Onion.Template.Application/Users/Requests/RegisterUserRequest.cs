﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
