﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
