using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Helpers;
using SharedClassLibrary.Models;

namespace Middle_Manager_Mobile.ViewModels
{

    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class ManageTaskTemplatesViewModel : ViewModelBase, IDisposable
    {
        private readonly TaskTemplateService _tasksService;
        private readonly IUserStorage _userStorage;
        private TaskTemplate _editableTaskTemplate;

        public TaskTemplate SelectedTaskTemplate { get; set; }

        private readonly TaskTemplate _editableUser;
        public ObservableCollection<TaskTemplate> TaskTemplatesList { get; set; }

        public TaskTemplate EditableTaskTemplate
        {
            get { return _editableTaskTemplate; }
            set
            {
                _editableTaskTemplate = value;
                OnPropertyChanged();
            }
        }

        public ManageTaskTemplatesViewModel()
        {
            //_navigation = navigation;
            _userStorage = UserStorage.Current;
            _tasksService = new TaskTemplateService();
            TaskTemplatesList = new ObservableCollection<TaskTemplate>();
            SelectedTaskTemplate = new TaskTemplate();
        }

        public async Task<bool> CreateTaskTemplate(TaskTemplate taskTemplate)
        {
            return await _tasksService.CreateTaskTemplate(taskTemplate);
        }

        public async Task<bool> DeleteTaskTemplate(int taskTemplateId)
        {
            return await _tasksService.DeleteTaskTemplate(taskTemplateId);
        }

        public async Task<bool> UpdateTaskTemplate(TaskTemplate taskTemplate)
        {
            return await _tasksService.UpdateTaskTemplate(taskTemplate);
        }

        public async Task<ObservableCollection<TaskTemplate>> GetTaskTemplates()
        {
            return (await _tasksService.GetTaskTemplates()).ToObservableCollection();
        }


        //TODO: can probs just cut all this dispose crap out into viewModelBase

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }
    }
}
