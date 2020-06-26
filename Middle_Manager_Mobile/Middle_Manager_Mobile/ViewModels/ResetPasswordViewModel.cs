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
    public class ResetPasswordViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authService;
        private string _emailAddress { get; set; }
        private int _verificationCode { get; set; }

        public ResetPasswordViewModel(string emailAddress, int verificationCode)
        {
            _authService = new AuthenticationService();
            _emailAddress = emailAddress;
            _verificationCode = verificationCode;

        }

        public async Task<bool> ResetPassword(string password)
        {
            return await _authService.ResetPassword(_emailAddress, _verificationCode, password);
        }
    }
}
