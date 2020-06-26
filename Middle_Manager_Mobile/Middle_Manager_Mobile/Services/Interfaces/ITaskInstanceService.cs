using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedClassLibrary.Models;

namespace Middle_Manager_Mobile.Services.Interfaces
{
    public interface ITaskInstanceService
    {
        Task<TaskInstance> CreateTaskInstance(TaskInstance taskInstance);
        Task<bool> DeleteTaskInstance(int taskInstanceId);
        Task<bool> UpdateTaskInstance(TaskInstance taskInstance);
        Task<TaskInstance> GetSingleTaskInstance(int taskInstanceId);
        Task<IEnumerable<TaskInstance>> GetUserTaskInstance(int userId);
        Task<IEnumerable<TaskInstance>> GetLocationTaskInstance(int locationId);
        Task<IEnumerable<TaskInstance>> GetOrganisationTaskInstances();
        Task<IEnumerable<TaskInstance>> GetUsersLocationTaskInstances(int userId);
    }
}
