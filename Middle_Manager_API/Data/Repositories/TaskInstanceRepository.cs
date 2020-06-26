using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Data.Repositories
{
    public class TaskInstanceRepository : Repository<TaskInstance>, ITaskInstanceRepository
    {
        private readonly DataContext _context;

        public TaskInstanceRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<TaskInstance> GetTaskInstances(int organisationId)
        {
            return FindByCondition(taskInstance => taskInstance.OrganisationId == organisationId);
        }
    }
}
