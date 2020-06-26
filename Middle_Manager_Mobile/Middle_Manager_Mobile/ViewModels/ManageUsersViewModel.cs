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
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.ViewModels
{
    public class ManageUsersViewModel : ViewModelBase, IDisposable
    {
        private readonly IUserStorage _userStorage;
        private readonly UserService _userService;
        private readonly AuthenticationService _authService;
        public UserForDetailedDto SelectedUser { get; set; }

        private UserForDetailedDto _editableUser;
        public ObservableCollection<UserForDetailedDto> UsersList { get; set; }
        public bool EmailSet => !String.IsNullOrEmpty(EditableUser.Email);


        public UserForDetailedDto EditableUser
        {
            get { return _editableUser; }
            set
            {
                _editableUser = value;
                OnPropertyChanged();
            }
        }

        public ManageUsersViewModel()
        {
            _userStorage = UserStorage.Current;
            _userService = new UserService();
            _authService = new AuthenticationService();
            UsersList = new ObservableCollection<UserForDetailedDto>();
            SelectedUser = new UserForDetailedDto();
        }

        public async Task<ObservableCollection<UserForDetailedDto>> GetUsers()
        {
            return (await _userService.GetUsers(UserRole.Employee)).ToObservableCollection();
        }

        public async Task<bool> DeleteUser(int userToDeleteId)
        {
            return (await _userService.DeleteUser(userToDeleteId));
        }

        public async Task<bool> RegisterInvite(UserForRegisterDto user)
        {
            user.OrganisationId = _userStorage.OrganisationId;
            return await _authService.RegisterInvite(user);
        }

        public async Task<bool> UpdateUser(UserForDetailedDto user)
        {
            user.OrganisationId = _userStorage.OrganisationId;
            return await _userService.UpdateUser(user);
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
