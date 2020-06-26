using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Services.Auth
{
    public class AuthService : ServiceBaseHelper, IAuthService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(IRepositoryWrapper repo,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Register(UserForRegisterDto userForRegisterDto)
        {
            if (await _repo.Auth.UserExists(userForRegisterDto.UserName))
                return false;

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            //await _repo.Auth.Register(userToCreate, userForRegisterDto.Password);

            return result.Succeeded;
        }

        public async Task<bool> UpdatePassword(User user, string newPassword)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result.Succeeded;
        }

        public async Task<User> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, userForLoginDto.Password, false, false);
                if (signInResult.Succeeded)
                    return user;
            }

            return null;
        }

        public async Task<User> FindByEmail(string emailAddress)
        {
            var userFromEmails = await _repo.AppUsers.FindAllAsync(e => e.Email.ToLower() == emailAddress.ToLower());
            if (userFromEmails.Count > 1 || userFromEmails.Count <= 0)
                return null;
            else
                return userFromEmails.FirstOrDefault();
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            return;
        }
    }
}
