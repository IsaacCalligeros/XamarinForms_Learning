using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Services.Interfaces;
using SharedClassLibrary.Models;

namespace Middle_Manager_Mobile.Services
{
    public class TaskInstanceService : BaseService, ITaskInstanceService
    {
        public async Task<TaskInstance> CreateTaskInstance(TaskInstance taskInstance)
        {
            return await MMApi.CreateTaskInstance(taskInstance);
        }

        public async Task<bool> UpdateTaskInstance(TaskInstance taskInstance)
        {
            return await MMApi.UpdateTaskInstance(taskInstance);
        }

        public async Task<bool> DeleteTaskInstance(int taskInstanceId)
        {
            return await MMApi.DeleteTaskInstance(taskInstanceId);
        }

        public async Task<TaskInstance> GetSingleTaskInstance(int taskInstanceId)
        {
            return await MMApi.GetSingleTaskInstance(taskInstanceId);
        }

        public async Task<IEnumerable<TaskInstance>> GetUserTaskInstance(int userId)
        {
            return await MMApi.GetUserTaskInstance(userId);
        }

        public async Task<IEnumerable<TaskInstance>> GetLocationTaskInstance(int locationId)
        {
            return await MMApi.GetLocationTaskInstance(locationId);
        }

        public async Task<IEnumerable<TaskInstance>> GetOrganisationTaskInstances()
        {
            return await MMApi.GetOrganisationTaskInstances();
        }

        public async Task<IEnumerable<TaskInstance>> GetUsersLocationTaskInstances(int userId)
        {
            return await MMApi.GetUsersLocationTaskInstances(userId);
        }
    }
}
