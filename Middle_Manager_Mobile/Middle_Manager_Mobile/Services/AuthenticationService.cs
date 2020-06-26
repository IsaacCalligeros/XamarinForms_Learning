using Middle_Manager_Mobile.Services.Interfaces;
using Newtonsoft.Json;
using SharedClassLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using System.Reactive.Linq;

namespace Middle_Manager_Mobile.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {

        public async Task<bool> Register(UserForRegisterDto user)
        {
            var res = await MMApi.Register(user);
            return res;
        }

        public async Task<bool> RegisterInvite(UserForRegisterDto user)
        {
            var res = await MMApi.RegisterInvite(true, user);
            return res;
        }


        public async Task<UserForSignedInDto> Login(UserForLoginDto user)
        {
            var userDetails = await MMApi.Login(user);
            return userDetails;
        }

        public async Task Logout()
        {
            await MMApi.Logout();
            await ClearCache();
            return;
        }

        public async Task<UserForSignedInDto> CheckValidToken(string mobileAccessToken)
        {
            return await MMApi.CheckValidToken(mobileAccessToken);
        }

        public async Task<bool> ResetPassword(string email, string password)
        {
            return await MMApi.ResetPassword(email, password);
        }

        public async Task<bool> CheckVerificationCode(string email, int verificationCode)
        {
            return await MMApi.CheckVerificationCode(email, verificationCode);
        }

        public async Task ClearCache()
        {
            await GetBlobCache().InvalidateAll();
        }

        public async Task<bool> ResetPassword(string email, int verificationCode, string password)
        {
            return await MMApi.ResetPassword(email, verificationCode, password);
        }
    }
}