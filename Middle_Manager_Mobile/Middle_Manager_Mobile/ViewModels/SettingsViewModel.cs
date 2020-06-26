using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.Services;

namespace Middle_Manager_Mobile.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly AuthenticationService _authService;
        private readonly IUserStorage _userStorage;
        private bool _registered = false;

        public SettingsViewModel()
        {
            _userStorage = UserStorage.Current;
            _authService = new AuthenticationService();
        }

        public async Task Logout()
        {
            await _authService.Logout();
        }


    }
}
