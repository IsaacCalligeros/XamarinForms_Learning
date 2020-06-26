using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_API.Services
{
    public class UsersService : ServiceBaseHelper, IUsersService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public UsersService(IRepositoryWrapper repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserForDetailedDto>> GetUsers(int organisationId, UserRole role)
        {
            var users = await _repo.AppUsers.GetUsers(organisationId, role);
            var userToReturn = _mapper.Map<IEnumerable<UserForDetailedDto>>(users);
            return userToReturn;
        }

        public async Task<IEnumerable<UserForDetailedDto>> GetLocationUsers(int locationId)
        {
            var users = await _repo.AppUsers.GetLocationUsers(locationId);
            var userToReturn = _mapper.Map<IEnumerable<UserForDetailedDto>>(users);
            return userToReturn;
        }

        public async Task<UserForDetailedDto> GetUser(int userId)
        {
            var user = await _repo.AppUsers.GetUser(userId);
            return _mapper.Map<UserForDetailedDto>(user);
        }

        public async Task<bool> UpdateUser(string userName, int userId, UserForDetailedDto userForUpdate)
        {
            var userFromRepo = await _repo.AppUsers.GetUser(userId);

            _mapper.Map(userForUpdate, userFromRepo);

            if (await _repo.SaveAsync(userName))
                return true;
            else return false;
        }

        public async Task<bool> DeleteUser(string userName, int userId, int userToDeleteId)
        {
            var userFromRepo = await _repo.AppUsers.GetUser(userToDeleteId);

            _repo.AppUsers.Delete(userFromRepo);
            if (await _repo.SaveAsync(userName))
                return true;
            return false;
        }

        public User FindByEmail(string emailAddress)
        {
            return _repo.AppUsers.FindByCondition(u => u.Email.ToLower() == emailAddress.ToLower()).FirstOrDefault();
        }
    }
}
