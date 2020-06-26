using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_API.Data.Interfaces
{
    public interface ITaskTemplateRepository : IRepository<TaskTemplate>
    {
        Task<TaskTemplate> GetTask(int taskId);
        Task<IEnumerable<TaskTemplate>> GetOrganisationTaskTemplates(int organisationId);
        bool hasActiveTaskInstances(int taskTemplateId);
        //public Task<TaskTemplate> GetTask(int ownerId, int taskId);

        //public Task<IEnumerable<TaskTemplate>> GetTasks(int userId, TaskAccessType type);
    }
}
