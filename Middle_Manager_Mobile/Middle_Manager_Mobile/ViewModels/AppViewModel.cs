using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Services;
using Middle_Manager_Mobile.Services.Interfaces;
using SharedClassLibrary;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Helpers;
using SharedClassLibrary.Models;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;


namespace Middle_Manager_Mobile.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private readonly IUserStorage _userStorage;
        private readonly IAuthenticationService _AuthService;

        public AppViewModel()
        {
            _userStorage = UserStorage.Current;
            _AuthService = new AuthenticationService();
        }

        public async Task<bool> CheckValidToken()
        {
            var signedInUser = await _AuthService.CheckValidToken(_userStorage.MobileAccessToken);

            if (signedInUser != null)
            {
                _userStorage.UserName = signedInUser.UserName;
                _userStorage.UserRole = signedInUser.UserRole;
                _userStorage.UserId = signedInUser.UserId;
                _userStorage.MobileAccessToken = signedInUser.Token;
                _userStorage.OrganisationId = signedInUser.OrganisationId;
                return true;
            }
            return false;
        }
    }
}

