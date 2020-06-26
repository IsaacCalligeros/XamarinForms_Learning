using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Middle_Manager_API.Helpers.Interfaces;
using Middle_Manager_API.Services;
using Middle_Manager_API.Services.Tasks;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskTemplatesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITaskTemplateService _taskTemplateService;
        private readonly ITaskInstanceService _taskInstanceService;
        private readonly IUsersService _usersService;

        public TaskTemplatesController(ITaskTemplateService tasksService,
            ITaskInstanceService taskInstanceService,
            IUsersService usersService,
            IRepositoryWrapper repo,
            IMapper mapper)
        {
            _mapper = mapper;
            _taskTemplateService = tasksService;
            _taskInstanceService = taskInstanceService;
            _usersService = usersService;
        }

        [HttpPost("CreateTaskTemplate")]
        public async Task<bool> CreateTaskTemplate(TaskTemplate taskTemplate)
        {
            return await _taskTemplateService.CreateTaskTemplate(UserDetails.Name, taskTemplate);

        }

        [HttpPost("DeleteTaskTemplate")]
        public async Task<bool> DeleteTaskTemplate(int taskTemplateId)
        {
            return await _taskTemplateService.DeleteTaskTemplate(UserDetails.Name, taskTemplateId);
        }

        [HttpPut("UpdateTaskTemplate")]
        public async Task<bool> UpdateTaskTemplate(TaskTemplate taskTemplate)
        {
            if(_taskTemplateService.HasAnyTaskInstances(taskTemplate.TaskTemplateId))
            {
                return await _taskTemplateService.UpdateTaskTemplate(UserDetails.Name, taskTemplate);
            }
            else
            {
                return false;
            }
        }

        [HttpGet("GetTaskTemplates")]
        public async Task<IEnumerable<TaskTemplate>> GetTaskTemplates()
        {
            var organisationTaskTemplates = await _taskTemplateService.GetOrganisationTaskTemplates(UserDetails.OrganisationId);
            return organisationTaskTemplates;
        }

        //[HttpGet("GetTaskTemplate")]
        //Task<TaskTemplate> GetTaskTemplate(int userId, int taskTemplateId)
        //{

        //}

        //[HttpGet]
        //public async Task<IEnumerable<TaskTemplate>> GetUserAssignedTasks(int userId, TaskAccessType type)
        //{
        //    var tasks = await _tasksService.GetUserAssignedTasks(userId, type);
        //    return tasks;
        //}



    }
}