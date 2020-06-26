using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Data.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private readonly DataContext _context;

        public LocationRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetLocations(int userId)
        {
            var locations = _context.LocationUsers.Where(l => l.UserId == userId).Select(lu => lu.LocationId);
            return await _context.Locations.Where(l => locations.Contains(l.LocationId)).ToListAsync();
        }

        public async Task<IEnumerable<Location>> GetOrganisationLocations(int organisationId)
        {
            return await _context.Locations.Where(l => l.OrganisationId == organisationId).ToListAsync();
        }
        public async Task<Location> GetLocation(int locationId)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == locationId);
        }
    }
}
