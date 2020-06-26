using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Middle_Manager_API.Data.Interfaces;
using Middle_Manager_API.Services;
using Middle_Manager_API.Services.Tasks;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskInstanceController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITaskInstanceService _taskInstanceService;
        private readonly IUsersService _usersService;
        private readonly IRepositoryWrapper _repo;

        public TaskInstanceController(ITaskInstanceService taskInstanceService,
            IUsersService usersService,
            IMapper mapper,
            IRepositoryWrapper repo)
        {
            _mapper = mapper;
            _taskInstanceService = taskInstanceService;
            _usersService = usersService;
            _repo = repo;
        }

        [HttpPost("CreateTaskInstance")]
        public async Task<TaskInstance> CreateTaskInstance(TaskInstance taskInstance)
        {
            return await _taskInstanceService.CreateTaskInstance(UserDetails.Name, taskInstance);
        }

        [HttpPost("DeleteTaskInstance")]
        public async Task<bool> DeleteTaskInstance(int taskInstanceId)
        {
            return await _taskInstanceService.DeleteTaskInstance(UserDetails.Name, taskInstanceId);
        }

        [HttpPut("UpdateTaskInstance")]
        public async Task<bool> UpdateTaskInstance(TaskInstance taskInstance)
        {
            return await _taskInstanceService.UpdateTaskInstance(UserDetails.Name, taskInstance);
        }

        [HttpGet("GetUserTaskInstance")]
        public async Task<IEnumerable<TaskInstance>> GetUserTaskInstance(int userId)
        {
            return await _taskInstanceService.GetTaskInstanceQueryable(s => s.AssignedToId == userId)
            .Include(t => t.TaskTemplate)
            .Include(u => u.AssignedTo)
            .ToListAsync();
        }

        [HttpGet("GetLocationTaskInstance")]
        public async Task<IEnumerable<TaskInstance>> GetLocationTaskInstance(int locationId)
        {
            return await _taskInstanceService.GetTaskInstanceQueryable(s => s.LocationId == locationId)
            .Include(t => t.TaskTemplate).ToListAsync();
        }

        [HttpGet("GetUsersLocationTaskInstances")]
        public async Task<IEnumerable<TaskInstance>> GetUsersLocationTaskInstances(int userId)
        {
            var fullUserDetails = await _usersService.GetUser(userId);
            var userLocations = await _repo.LocationUser.FindAll()
                .Where(l => l.UserId == userId)
                .Select(lu => lu.LocationId).ToListAsync();

            return await _taskInstanceService.GetTaskInstanceQueryable(s => s.LocationId.HasValue 
                                                                            && userLocations.Contains(s.LocationId.Value))
                .Include(tt => tt.TaskTemplate)
                .Include(l => l.Location)
                .ToListAsync();
        }

        [HttpGet("GetOrganisationTaskInstances")]
        public async Task<IEnumerable<TaskInstance>> GetOrganisationTaskInstances()
        {
            return await _taskInstanceService.GetTaskInstanceWithTemplates(UserDetails.OrganisationId);
        }
    }
}
