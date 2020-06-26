using Microsoft.AspNetCore.Mvc;
using Refit;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.Services
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    [Headers("Accept: application/json")]
    public interface ImmApi
    {
        #region Auth API
        [Post("/Auth/register")]
        Task<bool> Register([Body] UserForRegisterDto userForRegisterDto);
        [Post("/Auth/registerInvite")]
        Task<bool> RegisterInvite(bool sendInviteEmail, [Body] UserForRegisterDto userForRegisterDto);
        [Post("/Auth/login")]
        Task<UserForSignedInDto> Login(UserForLoginDto userForLoginDto);
        [Post("/Auth/logout")]
        Task Logout();
        [Get("/Auth/CheckValidToken")]
        Task<UserForSignedInDto> CheckValidToken(string mobileAccessToken);
        [Post("/Auth/ResetPassword")]
        Task<bool> ResetPassword(string email, string password);
        [Get("/Auth/CheckVerificationCode")]
        Task<bool> CheckVerificationCode(string email, int verificationCode);
        [Get("/Auth/ResetPassword")]
        Task<bool> ResetPassword(string email, int verificationCode, string password);
        #endregion

        #region users
        [Delete("/Users/DeleteUser")]
        Task<bool> DeleteUser(int userToDeleteId);
        [Get("/Users/GetUsers")]
        Task<IEnumerable<UserForDetailedDto>> GetUsers(UserRole role);
        [Get("/Users/GetUser")]
        Task<User> GetUser(int userId);
        [Delete("/Users/UpdateUser")]
        Task<bool> UpdateUser([Body] UserForDetailedDto userForUpdate);
        [Put("/Users/ForgotPassword")]
        Task<bool> ForgotPassword(string emailAddress);
        #endregion

        #region Photos
        [Get("/Photos/GetPhoto")]
        Task<PhotosForReturnDto> GetPhoto(int photoId);
        [Post("/Photos/AddPhoto")]
        Task<PhotosForReturnDto> AddPhoto([Body] PhotoForCreationDto photoForCreationDto);
        //[Post("/Photo/SetMainPhoto")]
        //Task<bool> SetMainPhoto(int photoId);
        [Delete("/Photos/DeletePhoto")]
        Task<bool> DeletePhoto(int photoId);

        #endregion

        #region TaskTemplates
        [Post("/TaskTemplates/CreateTaskTemplate")]
        Task<bool> CreateTaskTemplate([Body] TaskTemplate taskTemplate);
        [Post("/TaskTemplates/DeleteTaskTemplate")]
        Task<bool> DeleteTaskTemplate(int taskTemplateId);
        [Put("/TaskTemplates/UpdateTaskTemplate")]
        Task<bool> UpdateTaskTemplate([Body] TaskTemplate taskTemplate);
        [Get("/TaskTemplates/GetTaskTemplates")]
        Task<IEnumerable<TaskTemplate>> GetTaskTemplates();
        [Get("/TaskTemplates/GetTaskTemplate")]
        Task<TaskTemplate> GetTaskTemplate(int taskId);
        [Get("/TaskTemplates/GetUserAssignedTaskTemplates")]
        Task<TaskTemplate> GetUserAssignedTaskTemplates(TaskAccessType type);
        #endregion

        #region TaskInstance
        [Post("/TaskInstance/CreateTaskInstance")]
        Task<TaskInstance> CreateTaskInstance([Body] TaskInstance taskInstance);
        [Post("/TaskInstance/DeleteTaskInstance")]
        Task<bool> DeleteTaskInstance(int taskInstanceId);
        [Put("/TaskInstance/UpdateTaskInstance")]
        Task<bool> UpdateTaskInstance([Body] TaskInstance taskInstance);
        [Get("/TaskInstance/GetSingleTaskInstance")]
        Task<TaskInstance> GetSingleTaskInstance(int taskInstanceId);
        [Get("/TaskInstance/GetUserTaskInstance")]
        Task<IEnumerable<TaskInstance>> GetUserTaskInstance(int userId);
        [Get("/TaskInstance/GetLocationTaskInstance")]
        Task<IEnumerable<TaskInstance>> GetLocationTaskInstance(int locationId);
        [Get("/TaskInstance/GetOrganisationTaskInstances")]
        Task<IEnumerable<TaskInstance>> GetOrganisationTaskInstances();
        [Get("/TaskInstance/GetUsersLocationTaskInstances")]
        Task<IEnumerable<TaskInstance>> GetUsersLocationTaskInstances(int userId);
        #endregion

        #region Location
        [Put("/Location/CreateLocation")]
        Task<bool> CreateLocation([Body] Location location);
        [Put("/Location/UpdateLocation")]
        Task<bool> UpdateLocation([Body] Location location);
        [Delete("/Location/DeleteLocation")]
        Task<bool> DeleteLocation(int locationId);
        [Get("/Location/GetLocation")]
        Task<Location> GetLocation(int locationId);
        [Get("/Location/GetLocations")]
        Task<IEnumerable<Location>> GetLocations();
        [Get("/Location/GetOrganisationLocations")]
        Task<IEnumerable<Location>> GetOrganisationLocations();

        #endregion

        #region Locationuser

        [Put("/LocationUser/CreateLocationUser")]
        Task<bool> CreateLocationUser(LocationUser locationUser);

        [Delete("/LocationUser/DeleteLocationUser")]
        Task<bool> DeleteLocationUser(int locationId, int userToDeleteId);

        [Get("/LocationUser/GetLocationUsers")]
        Task<IEnumerable<LocationUser>> GetLocationUsers(int locationId);

        [Get("/LocationUser/GetLocationUser")]
        Task<LocationUser> GetLocationUser(int locationUserId);

        [Put("/LocationUser/UpdateLocationUser")]
        Task<bool> UpdateLocationUser(LocationUser locationUser);


        #endregion

    }
}
