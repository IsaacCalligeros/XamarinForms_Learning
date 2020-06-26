using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middle_Manager_API.Services;
using Middle_Manager_API.Services.Locations;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationUserController : BaseController
    {
        private readonly ILocationUserService _locationUserService;

        public LocationUserController(ILocationUserService locationUserService)
        {
            _locationUserService = locationUserService;
        }

        [HttpPut("CreateLocationUser")]
        public async Task<bool> CreateLocationUser(LocationUser locationUser)
        {
            return await _locationUserService.CreateLocationUser(UserDetails.Name, locationUser);
        }

        [HttpDelete("DeleteLocationUser")]
        public async Task<bool> DeleteLocationUser(int locationId, int userToDeleteId)
        {
            return await _locationUserService.DeleteLocationUser(UserDetails.Name, userToDeleteId, locationId);
        }

        [HttpGet("GetLocationUsers")]
        public async Task<IEnumerable<LocationUser>> GetLocationUsers(int locationId)
        {
            return await _locationUserService.GetLocationUsers(locationId);
        }


        [HttpGet("GetLocationUser")]
        public async Task<LocationUser> GetLocationUser(int locationUserId)
        {
            return await _locationUserService.GetUser(locationUserId);
        }

        [HttpPut("UpdateLocationUser")]
        public async Task<bool> UpdateLocationUser(LocationUser locationUser)
        {
            return await _locationUserService.UpdateLocationUser(UserDetails.Name, locationUser);
        }
    }
}
