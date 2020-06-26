using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Services.Tasks
{
    public interface ITaskInstanceService
    {
        public Task<TaskInstance> CreateTaskInstance(string userName, TaskInstance taskInstance);

        public Task<bool> DeleteTaskInstance(string userName, int taskInstanceId);

        public Task<bool> UpdateTaskInstance(string userName, TaskInstance taskTemplate);

        IQueryable<TaskInstance> GetTaskInstanceQueryable(Expression<Func<TaskInstance, bool>> findCondition);

        Task<ICollection<TaskInstance>> GetTaskInstanceWithTemplates(int organisationId);

    }
}
