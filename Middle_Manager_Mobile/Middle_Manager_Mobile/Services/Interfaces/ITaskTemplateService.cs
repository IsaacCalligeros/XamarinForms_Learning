using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedClassLibrary.Models;

namespace Middle_Manager_Mobile.Services.Interfaces
{
    public interface ITaskTemplateService
    {
        Task<bool> CreateTaskTemplate(TaskTemplate taskTemplate);
        Task<bool> UpdateTaskTemplate(TaskTemplate taskTemplate);
        Task<bool> DeleteTaskTemplate(int taskTemplateId);
        Task<IEnumerable<TaskTemplate>> GetTaskTemplates();
        Task<TaskTemplate> GetTaskTemplate(int taskTemplateId);
    }
}
