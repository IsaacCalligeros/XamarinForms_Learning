using Microsoft.EntityFrameworkCore;
using Middle_Manager_API.Data;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middle_Manager_API.Data.Repositories
{
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _context.AppUsers.AnyAsync(x => x.UserName == userName))
                return true;
            return false;
        }
    }
}
