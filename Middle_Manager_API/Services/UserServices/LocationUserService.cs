using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Services
{
    public class LocationUserService : ServiceBaseHelper, ILocationUserService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public LocationUserService(IRepositoryWrapper repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationUser>> GetLocationUsers(int locationId)
        {
            return await _repo.LocationUser.FindAll()
                .Include(u => u.User)
                .Where(l => l.LocationId == locationId)
                .ToListAsync();
        }

        public async Task<LocationUser> GetUser(int locationUserId)
        {
            return await _repo.LocationUser.FindAll().SingleOrDefaultAsync(l => l.LocationUserId == locationUserId);
        }

        public async Task<bool> UpdateLocationUser(string userName, LocationUser locationUser)
        {
            var locationUserFromRepo = await _repo.LocationUser
                .FindAll().SingleOrDefaultAsync(l => l.LocationUserId == locationUser.LocationUserId);

            _mapper.Map(locationUser, locationUserFromRepo);

            if (await _repo.SaveAsync(userName))
                return true;
            else return false;
        }

        public async Task<bool> DeleteLocationUser(string userName, int locationId, int userId)
        {
            var userFromRepo = await _repo.LocationUser.FindAll()
                .Where(l => l.UserId == userId &&
                            l.LocationId == locationId).ToListAsync();

            _repo.LocationUser.DeleteRange(userFromRepo);
            if (await _repo.SaveAsync(userName))
                return true;
            return false;
        }

        public async Task<bool> CreateLocationUser(string userName, LocationUser locationUser)
        {
            _repo.LocationUser.Create(locationUser);

            if (await _repo.SaveAsync(userName))
            {
                return true;
            }

            throw new Exception("Error Creating the Location");
        }
    }
}
