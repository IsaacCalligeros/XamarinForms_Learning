using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedClassLibrary;
using SharedClassLibrary.Models;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_API.Services.Tasks
{
    public interface ITaskTemplateService
    {
        Task<bool> CreateTaskTemplate(string userName, TaskTemplate taskTemplate);
        Task<bool> DeleteTaskTemplate(string userName, int taskId);
        //Task<IEnumerable<TaskTemplate>> GetUserAssignedTasks(int userId, TaskAccessType type);
        Task<bool> UpdateTaskTemplate(string userName, TaskTemplate taskTemplate);

        Task<IEnumerable<TaskTemplate>> GetOrganisationTaskTemplates(int organisationId);
        bool HasAnyTaskInstances(int taskTemplateId);

    }
}
