using Middle_Manager_Mobile.Helpers;
using Middle_Manager_Mobile.Helpers.Interfaces;
using Middle_Manager_Mobile.Services;
using Middle_Manager_Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Middle_Manager_Mobile.ViewModels
{
    public class ForgotPasswordViewModel : ViewModelBase
    {
        private readonly IUserStorage _userStorage;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;

        public ForgotPasswordViewModel()
        {
            _userStorage = UserStorage.Current;
            _userService = new UserService();
            _authService = new AuthenticationService();
        }

        public async Task<bool> OnEmailVerificationButtonClicked(string emailAddress)
        {
            return await _userService.ForgotPassword(emailAddress);
        }

        public async Task<bool> OnResetButtonClicked(string email, int verificationCode)
        {
            return await _authService.CheckVerificationCode(email, verificationCode);
        }
    }
}
