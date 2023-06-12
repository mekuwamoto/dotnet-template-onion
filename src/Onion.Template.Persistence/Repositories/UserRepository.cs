using Onion.Template.Application.Commom.Interfaces.Repositories;
using Onion.Template.Domain.Entities;
using Onion.Template.Persistence.Contexts;
using Onion.Template.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext _context;

        public UserRepository(IUserContext context)
        {
            _context = context;
        }

        public Task<string> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
