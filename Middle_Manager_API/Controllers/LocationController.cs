using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middle_Manager_API.Data.Interfaces;
using Middle_Manager_API.Services.Locations;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILocationsService _locationsService;

        public LocationController(IMapper mapper,
            ILocationsService locationsService)
        {
            _locationsService = locationsService;
        }

        [HttpPut("CreateLocation")]
        public async Task<bool> CreateLocation(Location location)
        {
            return await _locationsService.CreateLocation(UserDetails.Name, location);
        }

        [HttpDelete("DeleteLocation")]
        public async Task<bool> DeleteLocation(int locationId)
        {
            return await _locationsService.DeleteLocation(UserDetails.Name, locationId);
        }

        [HttpGet("GetLocations")]
        public async Task<IEnumerable<Location>> GetLocations()
        {

            return await _locationsService.GetLocations(UserDetails.Name, UserDetails.Id);
        }

        [HttpGet("GetOrganisationLocations")]
        public async Task<IEnumerable<Location>> GetOrganisationLocations()
        {
            return await _locationsService.GetOrganisationLocations(UserDetails.OrganisationId);
        }

        [HttpGet("GetLocation")]
        public async Task<Location> GetLocation(int locationId)
        {
            return await _locationsService.GetLocation(UserDetails.Name, locationId);
        }

        [HttpPut("UpdateLocation")]
        public async Task<bool> UpdateLocation(Location location)
        {
            return await _locationsService.UpdateLocation(UserDetails.Name, location);
        }
    }
}
