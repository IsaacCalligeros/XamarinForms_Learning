using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_API.Services.Tasks
{
    public class TaskTemplateService : ServiceBaseHelper, ITaskTemplateService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public TaskTemplateService(IRepositoryWrapper repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> CreateTaskTemplate(string userName, TaskTemplate taskTemplate)
        {
            _repo.TaskTemplate.Create(taskTemplate);

            if (await _repo.SaveAsync(userName))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteTaskTemplate(string userName, int taskTemplateId)
        {
            var taskTemplateFromRepo = await _repo.TaskTemplate.GetTask(taskTemplateId);
            _repo.TaskTemplate.Delete(taskTemplateFromRepo);

            if (await _repo.SaveAsync(userName))
                return true;

            return false;
        }

        //public async Task<IEnumerable<TaskTemplate>> GetUserAssignedTasks(int userId)
        //{
        //    return await _repo.TaskTemplate.GetTasks(userId);
        //}

        public async Task<bool> UpdateTaskTemplate(string userName, TaskTemplate taskTemplate)
        {
            //Want to do a generic FindAllAsync on the repository level, i think this makes no difference though, i'm just ToListAsyncing at repo level compared to
            var taskTemplateFromRepo = await _repo.TaskTemplate.GetTask(taskTemplate.TaskTemplateId);

            _mapper.Map(taskTemplate, taskTemplateFromRepo);

            if (await _repo.SaveAsync(userName))
                return true;
            return false;
        }

        public async Task<IEnumerable<TaskTemplate>> GetOrganisationTaskTemplates(int organisationId)
        {
            return await _repo.TaskTemplate.GetOrganisationTaskTemplates(organisationId);
        }

        public bool HasAnyTaskInstances(int taskTemplateId)
        {
            return _repo.TaskTemplate.hasActiveTaskInstances(taskTemplateId);
        }
    }
}
