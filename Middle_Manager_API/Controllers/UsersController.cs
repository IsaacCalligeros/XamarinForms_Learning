using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Linq;
using System.Net;
using System.Runtime.Versioning;
using Middle_Manager_API.Controllers;
using Middle_Manager_API.Data.Interfaces;
using Middle_Manager_API.Data.Repositories;
using Middle_Manager_API.Helpers.Interfaces;
using Middle_Manager_API.Services;
using Middle_Manager_API.Services.Auth;
using SharedClassLibrary;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using System.Linq.Expressions;
using static SharedClassLibrary.Enums;

namespace MiddleManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _userService;
        private readonly IAuthService _authService;
        private readonly IEmailHelper _emailhelper;

        public UsersController(IMapper mapper,
            IUsersService userService,
            IAuthService authService,
            IRepositoryWrapper repo,
            IEmailHelper emailhelper)
        {
            _mapper = mapper;
            _userService = userService;
            _authService = authService;
            _emailhelper = emailhelper;
        }

        [HttpPut("UpdateUser")]
        public async Task<bool> UpdateUser(int userForUpdateId, UserForDetailedDto userForUpdateDto)
        {
            var saved = await _userService.UpdateUser(UserDetails.Name, userForUpdateId, userForUpdateDto);
            if (saved)
                return true;

            throw new Exception($"Updating user {userForUpdateId} failed on save");
        }

        [HttpDelete("DeleteUser")]
        public async Task<bool> DeleteUser(int userToDeleteId)
        {
            var deletedUser = await _userService.DeleteUser(UserDetails.Name, UserDetails.Id, userToDeleteId);
            if (deletedUser)
                return true;
            return false;
        }

        [HttpGet("GetUsers")]
        public async Task<IEnumerable<UserForDetailedDto>> GetUsers(UserRole role)
        {
            return await _userService.GetUsers(UserDetails.OrganisationId, role);
        }

        [HttpGet("GetUser")]
        public async Task<UserForDetailedDto> GetUser(int userId)
        {
            var user = await _userService.GetUser(userId);
            return user;
        }

        [AllowAnonymous]
        [HttpPut("ForgotPassword")]
        public async Task<bool> ForgotPassword(string emailAddress)
        {
            var user = _userService.FindByEmail(emailAddress);
            if (user == null)
                return false;

            Random r = new Random();
            int randNum = r.Next(1000000);
            string sixDigitNumber = randNum.ToString("D6");

            var htmlContent = String.Format(SharedResources.EmailVarificationMessage, sixDigitNumber);
            var subject = SharedResources.PasswordResetCode;

            var emailSender = _emailhelper.SendEmail("Spam.Calligeros@gmail.com", SharedResources.MiddleManager, subject, user.Email, user.FirstName, String.Empty, htmlContent);
            var userDetails = new UserForDetailedDto();

            userDetails = _mapper.Map(user, userDetails);
            userDetails.PasswordResetEmailTimer = DateTime.UtcNow;
            userDetails.PasswordResetCode = sixDigitNumber;

            //Assume email is fired.
            var isUpdated = await _userService.UpdateUser(UserDetails.Name, user.UserId, userDetails);
            return isUpdated;
        }
    }
}