using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Data.Interfaces
{
    public interface ITaskInstanceRepository : IRepository<TaskInstance>
    {
        IQueryable<TaskInstance> GetTaskInstances(int organisationId);
    }
}
