using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_API.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<User>> GetUsers(int organisationId, UserRole role)
        {
            var users = await _context.AppUsers.Where(u => u.OrganisationId == organisationId 
                && u.UserRole == role || role == UserRole.All)
                .Include(p => p.Photos)
                .Include(lu => lu.LocationUsers)
                .ToListAsync();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.AppUsers
                .Include(p => p.Photos)
                .Include(l => l.LocationUsers)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetLocationUsers(int locationId)
        {
            var users = await _context.AppUsers.Include(l => l.LocationUsers)
                .Where(lid => lid.LocationUsers.Select(l => l.LocationId).Contains(locationId))
                .ToListAsync();
            return users;
        }

    }
}
