using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.Services;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Helpers;
using SharedClassLibrary.Models;

namespace Middle_Manager_Mobile.ViewModels
{
    public class LocationDetailsViewModel : ViewModelBase, IDisposable
    {
        private readonly IUserStorage _userStorage;
        private readonly LocationService _locationService;
        private readonly UserService _userService;
        private readonly LocationUserService _locationUserService;

        private Location _editableLocation;

        public Location EditableLocation
        {
            get { return _editableLocation; }
            set
            {
                _editableLocation = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Location> LocationsList { get; set; }
        public ObservableCollection<LocationUser> LocationUsersList { get; set; }
        public ObservableCollection<UserForDetailedDto> UsersList { get; set; }
        public Location SelectedLocation { get; set; }

        public LocationDetailsViewModel()
        {
            LocationsList = new ObservableCollection<Location>();
            LocationUsersList = new ObservableCollection<LocationUser>();
            SelectedLocation = new Location();
            _userStorage = UserStorage.Current;

            _locationService = new LocationService();
            _userService = new UserService();
            _locationUserService = new LocationUserService();
        }

        public async Task<bool> CreateLocationUser(LocationUser locationUser)
        {
            return await _locationUserService.CreateLocationUser(locationUser);
        }

        public async Task<bool> UpdateLocationUser(LocationUser locationUser)
        {
            return await _locationUserService.UpdateLocationUser(locationUser);
        }

        public async Task<bool> DeleteLocationUser(int locationId, int userToDeleteId)
        {
            return await _locationUserService.DeleteLocationUser(locationId, userToDeleteId);
        }

        public async Task<ObservableCollection<LocationUser>> GetLocationUsers(int locationId)
        {
            return (await _locationUserService.GetLocationUsers(locationId)).ToObservableCollection();
        }

        public async Task<ObservableCollection<UserForDetailedDto>> GetUsers()
        {
            return (await _userService.GetUsers()).ToObservableCollection();
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
