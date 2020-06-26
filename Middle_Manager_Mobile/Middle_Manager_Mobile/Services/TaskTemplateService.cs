using Middle_Manager_Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedClassLibrary.Models;

namespace Middle_Manager_Mobile.Services
{
    public class TaskTemplateService : BaseService, ITaskTemplateService
    {
        public async Task<bool> CreateTaskTemplate(TaskTemplate taskTemplate)
        {
            var res = await MMApi.CreateTaskTemplate(taskTemplate);
            return res;
        }

        public async Task<bool> UpdateTaskTemplate(TaskTemplate taskTemplate)
        {
            var res = await MMApi.UpdateTaskTemplate(taskTemplate);
            return res;
        }

        public async Task<bool> DeleteTaskTemplate(int taskTemplateId)
        {
            return await MMApi.DeleteTaskTemplate(taskTemplateId);
        }

        public async Task<IEnumerable<TaskTemplate>> GetTaskTemplates()
        {
            return await MMApi.GetTaskTemplates();
        }

        public async Task<TaskTemplate> GetTaskTemplate(int taskTemplateId)
        {
            return await MMApi.GetTaskTemplate(taskTemplateId);
        }


    }
}
