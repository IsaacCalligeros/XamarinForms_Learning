using System;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Middle_Manager_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Middle_Manager_API.Data.Interfaces;
using Middle_Manager_API.Helpers.Interfaces;
using Middle_Manager_API.Services.Auth;
using SharedClassLibrary;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using System.Linq;
using Middle_Manager_API.Services;

namespace Middle_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUsersService _usersService;
        private readonly IEmailHelper _emailHelper;
        private readonly IRepositoryWrapper _repo;

        public AuthController(IAuthService authService,
            IUsersService usersService,
            IEmailHelper emailHelper,
            IConfiguration config,
            IMapper mapper,
            IRepositoryWrapper repo)
        {
            _mapper = mapper;
            _config = config;
            _authService = authService;
            _usersService = usersService;
            _emailHelper = emailHelper;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<bool> Register(UserForRegisterDto userForRegisterDto)
        {
            return await _authService.Register(userForRegisterDto);
        }

        [HttpPost("registerInvite")]
        public async Task<bool> RegisterInvite(bool sendInviteEmail, UserForRegisterDto userForRegisterDto)
        {

            var htmlContent = String.Format(SharedResources.InviteEmailContent, UserDetails.Name, "Apple", "Android",
                userForRegisterDto.UserName, userForRegisterDto.Password);
            var subject = SharedResources.InvitedtoUseMiddleManager;

            var registerUser = await _authService.Register(userForRegisterDto);

            if (sendInviteEmail && registerUser)
            {
                await _emailHelper.SendEmail(UserDetails.Email, UserDetails.Name, subject, userForRegisterDto.Email, userForRegisterDto.FirstName, String.Empty, htmlContent);
            }
            return registerUser;
        }

        [HttpPost("login")]
        public async Task<UserForSignedInDto> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _authService.Login(userForLoginDto);
            if (userFromRepo == null)
                return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName),
                new Claim(ClaimTypes.Email, userFromRepo.Email),
                new Claim(ClaimTypes.PrimaryGroupSid, userFromRepo.OrganisationId.ToString()),
                new Claim(ClaimTypes.DateOfBirth, userFromRepo.DateofBirth.ToShortDateString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config.GetSection("ClaimSettings:Issuer").Value,
                Audience = _config.GetSection("ClaimSettings:Audience").Value,
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var user = _mapper.Map<UserForSignedInDto>(userFromRepo);

            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await _authService.Logout();
            return;
        }

        [HttpGet("CheckValidToken")]
        public async Task<UserForSignedInDto> CheckValidToken(string mobileAccessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(mobileAccessToken);
            var existingToken = handler.ReadToken(mobileAccessToken) as JwtSecurityToken;

            var confirmedUserId = int.Parse(existingToken.Claims.First(claim => claim.Type == "nameid").Value);

            var userFromRepo = _repo.AppUsers.FindById<User>(confirmedUserId);
            if (userFromRepo == null)
                return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName),
                new Claim(ClaimTypes.Email, userFromRepo.Email),
                new Claim(ClaimTypes.PrimaryGroupSid, userFromRepo.OrganisationId.ToString()),
                new Claim(ClaimTypes.DateOfBirth, userFromRepo.DateofBirth.ToShortDateString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config.GetSection("ClaimSettings:Issuer").Value,
                Audience = _config.GetSection("ClaimSettings:Audience").Value,
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var user = _mapper.Map<UserForSignedInDto>(userFromRepo);

            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        
        [HttpGet("CheckVerificationCode")]
        [AllowAnonymous]
        public async Task<bool> CheckVerificationCode(string email, int verificationCode)
        {
            var user = await _authService.FindByEmail(email);
            if (user == null)
                return false;

            if (user.PasswordResetCode == verificationCode.ToString())
                return true;
            
            return false;
        }

        [HttpGet("ResetPassword")]
        [AllowAnonymous]
        public async Task<bool> ResetPassword(string email, int verificationCode, string newPassword)
        {
            var user = await _authService.FindByEmail(email);
            if (user == null)
                return false;

            if (user.PasswordResetCode == verificationCode.ToString() && user.PasswordResetEmailTimer >= DateTime.UtcNow.AddMinutes(-5))
            {
                return await _authService.UpdatePassword(user, newPassword);
            }
            return false;
        }
    }
}