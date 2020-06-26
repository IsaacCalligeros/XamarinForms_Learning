using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Services.Tasks
{
    public class TaskInstanceService : ServiceBaseHelper, ITaskInstanceService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public TaskInstanceService(IRepositoryWrapper repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TaskInstance> CreateTaskInstance(string userName, TaskInstance taskInstance)
        {
            _repo.TaskInstance.Create(taskInstance);

            if (await _repo.SaveAsync(userName))
            {
                return taskInstance;
            }

            return null;
        }

        public async Task<bool> DeleteTaskInstance(string userName, int taskInstanceId)
        {
            var taskInstanceFromRepo = _repo.TaskInstance.FindById<TaskInstance>(taskInstanceId);
            _repo.TaskInstance.Delete(taskInstanceFromRepo);

            if (await _repo.SaveAsync(userName))
                return true;

            return false;
        }

        public async Task<bool> UpdateTaskInstance(string userName, TaskInstance taskInstance)
        {
            //Want to do a generic FindAllAsync on the repository level, i think this makes no difference though, i'm just ToListAsyncing at repo level compared to
            var taskInstanceFromRepo = _repo.TaskInstance.FindById<TaskInstance>(taskInstance.TaskInstanceId);

            _mapper.Map(taskInstance, taskInstanceFromRepo);

            if (await _repo.SaveAsync(userName))
                return true;
            return false;
        }

        public IQueryable<TaskInstance> GetTaskInstanceQueryable(Expression<Func<TaskInstance, bool>> findCondition)
        {
            return _repo.TaskInstance.FindAll().Where(findCondition);
        }

        public async Task<ICollection<TaskInstance>> GetTaskInstanceWithTemplates(int organisationId)
        {
            //return await _repo.TaskInstance.GetTaskInstances(organisationId).Include(tt => tt.TaskTemplate).ToListAsync();
            return _repo.TaskInstance.GetTaskInstances(organisationId)
                .Include(tt => tt.TaskTemplate)
                .Include(s => s.AssignedTo)
                .Include(l => l.Location)
                .ToList();
            //from taskInstance in taskInstances
            //where taskInstance.AssignedToId != null

            //select new TaskInstanceUserDetailsDto()
            //{
            //    TaskInstance = taskInstance,

            //}
        }
    }
}
