using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Middle_Manager_API.Data.Interfaces;
using Middle_Manager_API.Data.Repositories;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_API.Data.Repositories
{
    public class TaskTemplateRepository : Repository<TaskTemplate>, ITaskTemplateRepository
    {
        private readonly DataContext _context;
        public TaskTemplateRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TaskTemplate> GetTask(int taskTemplateId)
        {
            return await _context.TaskTemplates.FirstOrDefaultAsync(tt => tt.TaskTemplateId == taskTemplateId);
        }

        public async Task<IEnumerable<TaskTemplate>> GetOrganisationTaskTemplates(int organisationId)
        {
            return await _context.TaskTemplates.Where(tt => tt.OrganisationId == organisationId).ToListAsync();
        }

        public bool hasActiveTaskInstances(int taskTemplateId)
        {
            return _context.TaskTemplates
                .Where(tt => tt.TaskTemplateId == taskTemplateId)
                .Include(ti => ti.TaskInstances).FirstOrDefault()
                .TaskInstances.Any();
        }

        //public async Task<TaskTemplate> GetTask(int ownerId, int taskId)
        //{
        //    return await _context.TaskTemplates.Where(t => t.TaskId == taskId).FirstOrDefaultAsync(o => o.CreatedById == ownerId);
        //}

        //public async Task<IEnumerable<TaskTemplate>> GetTasks(int userId, TaskAccessType type)
        //{
        //    var tasks = Enumerable.Empty<TaskTemplate>();
        //    if (type == TaskAccessType.Owner)
        //    {
        //        //tasks = await _context.TaskTemplates.Where(t => t.OwnerId == userId).ToListAsync();
        //    }
        //    else if (type == TaskAccessType.Creator)
        //    {
        //        tasks = await _context.TaskTemplates.Where(t => t.CreatedById == userId).ToListAsync();
        //    }
        //    else if (type == TaskAccessType.Assignee)
        //    {
        //        tasks = await _context.TaskTemplates.Where(t => t.AssignedToId == userId).ToListAsync();
        //    }
        //    else return null;

        //    return tasks;
        //}
    }
}
