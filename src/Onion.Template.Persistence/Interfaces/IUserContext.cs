using Microsoft.EntityFrameworkCore;
using Onion.Template.Application.Commom.Enums;
using Onion.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Persistence.Interfaces;

public interface IUserContext
{
    DbSet<User> Users { get; set; }
}
