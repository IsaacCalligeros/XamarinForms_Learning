using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.Services;
using SharedClassLibrary;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Helpers;
using SharedClassLibrary.Models;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.ViewModels
{
    public class TaskInstanceViewModel : ViewModelBase, IDisposable
    {
        private readonly IUserStorage _userStorage;
        private readonly UserService _userService;
        private readonly LocationService _locationService;
        private readonly TaskInstanceService _taskInstanceService;
        private readonly TaskTemplateService _taskTemplateService;
        private readonly PhotoService _photoService;
        public TaskInstance SelectedTaskInstance { get; set; }

        private TaskInstance _editableTaskInstance;
        public ObservableCollection<TaskInstance> TaskInstanceList { get; set; }
        public ObservableCollection<TaskTemplate> TaskTemplateList { get; set; }

        private TaskTemplate _selectedTaskTemplate { get; set; }

        public TaskTemplate SelectedTaskTemplate
        {
            get { return _selectedTaskTemplate; }
            set
            {
                _selectedTaskTemplate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UserForDetailedDto> UserList { get; set; }
        public ObservableCollection<Location> LocationList { get; set; }

        public IList<TaskTarget> TaskTargetEnum => (IList<TaskTarget>)Enum.GetValues(typeof(TaskTarget));

        public IList<RecurrenceType> RecurrenceTypeEnum => (IList<RecurrenceType>)Enum.GetValues(typeof(RecurrenceType));

        public TaskInstance EditableTaskInstance
        {
            get { return _editableTaskInstance; }
            set
            {
                _editableTaskInstance = value;
                OnPropertyChanged();
            }
        }

        public TaskInstanceViewModel()
        {
            _userStorage = UserStorage.Current;
            _userService = new UserService();
            _locationService = new LocationService();
            _taskInstanceService = new TaskInstanceService();
            _taskTemplateService = new TaskTemplateService();
            _photoService = new PhotoService();

            TaskInstanceList = new ObservableCollection<TaskInstance>();
            TaskTemplateList = new ObservableCollection<TaskTemplate>();
            SelectedTaskTemplate = new TaskTemplate();
            UserList = new ObservableCollection<UserForDetailedDto>();
            LocationList = new ObservableCollection<Location>();
            SelectedTaskInstance = new TaskInstance();
        }

        public async Task<TaskInstance> GetSingleTaskInstanceInstance(int taskInstanceId)
        {
            return (await _taskInstanceService.GetSingleTaskInstance(taskInstanceId));
        }

        public async Task<ObservableCollection<TaskInstance>> GetLocationTaskInstance(int locationId)
        {
            return (await _taskInstanceService.GetLocationTaskInstance(locationId)).ToObservableCollection();
        }

        public async Task<ObservableCollection<TaskInstance>> GetOrganisationTaskInstance()
        {
            return (await _taskInstanceService.GetOrganisationTaskInstances()).ToObservableCollection();
        }

        public async Task<ObservableCollection<TaskInstance>> GetUserTaskInstance()
        {
            return (await _taskInstanceService.GetUserTaskInstance(_userStorage.UserId)).ToObservableCollection();
        }

        public async Task<TaskInstance> CreateTaskInstance(TaskInstance taskInstance = null)
        {
            var ti = EditableTaskInstance;
            ti.OrganisationId = _userStorage.OrganisationId;

            return await _taskInstanceService.CreateTaskInstance(ti);
        }

        public async Task<bool> DeleteTaskInstance(int taskInstanceId)
        {
            return await _taskInstanceService.DeleteTaskInstance(taskInstanceId);
        }

        public async Task<bool> UpdateTaskInstance(TaskInstance taskInstance)
        {
            return await _taskInstanceService.UpdateTaskInstance(taskInstance);
        }

        public async Task<ObservableCollection<TaskTemplate>> GetTaskTemplates()
        {
            return (await _taskTemplateService.GetTaskTemplates()).ToObservableCollection();
        }

        public async Task<ObservableCollection<UserForDetailedDto>> GetUsers()
        {
            return (await _userService.GetUsers()).ToObservableCollection();
        }

        public async Task<ObservableCollection<Location>> GetLocations()
        {
            return (await _locationService.GetLocations()).ToObservableCollection();
        }

        public async Task<PhotosForReturnDto> AddPhoto(PhotoForCreationDto photoForCreationDto)
        {
            return await _photoService.AddPhoto(photoForCreationDto);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #region IDisposable Support
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
        #endregion
    }
}
