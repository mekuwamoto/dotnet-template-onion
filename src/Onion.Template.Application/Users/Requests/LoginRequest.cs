﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Application.Users.Requests;

public class LoginRequest
{
	[Required, EmailAddress]
	public string Email { get; set; } = null!;
	[Required]
	public string Password { get; set; } = null!;
}
