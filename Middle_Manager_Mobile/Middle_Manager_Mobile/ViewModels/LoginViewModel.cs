using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedClassLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using Xamarin.Forms.Internals;
using Middle_Manager_Mobile.Pages;

namespace Middle_Manager_Mobile.ViewModels
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]   //TODO: Readup on this.
    public class LoginViewModel : ViewModelBase
    {
        private readonly AuthenticationService _authService;
        private readonly IUserStorage _userStorage;

        private UserForLoginDto _userForLoginDto;

        public UserForLoginDto UserForLogin
        {
            get { return _userForLoginDto; }
            set { SetValue(ref _userForLoginDto, value); }
        }

        public LoginViewModel()
        {
            _userStorage = UserStorage.Current;
            _authService = new AuthenticationService();
            UserForLogin = new UserForLoginDto();
        }

        public async Task<bool> Register(UserForRegisterDto user)
        {
            return await _authService.Register(user);
        }

        public async Task<bool> Login(UserForLoginDto user)
        {
            var userDetails = await _authService.Login(user);
            if (userDetails != null)
            {
                _userStorage.UserName = userDetails.UserName;
                _userStorage.UserRole = userDetails.UserRole;
                _userStorage.UserId = userDetails.UserId;
                _userStorage.MobileAccessToken = userDetails.Token;
                _userStorage.OrganisationId = userDetails.OrganisationId;
                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            _userStorage.IsLoggedIn = false;
            _userStorage.SessionToken = string.Empty;
            if (_userStorage.HasMobileAccessToken)
            {
                //TODO: Implement
            }
            else
            {
                await Reset();
            }
        }

        public async Task Reset()
        {
            _userStorage.IsLoggedIn = false;
            _userStorage.SessionToken = string.Empty;
            _userStorage.MobileAccessToken = string.Empty;
            _userStorage.UserName = string.Empty;
            await _authService.ClearCache();
        }
    }
}
