using SharedClassLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Middle_Manager_Mobile.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Register(UserForRegisterDto user);
        Task<UserForSignedInDto> CheckValidToken(string mobileAccessToken);
        public Task<bool> ResetPassword(string email, int verificationCode, string password);
        Task<bool> CheckVerificationCode(string email, int verificationCode);
    }
}
