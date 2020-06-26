using System;
using System.Collections.Generic;
using System.Text;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.Helpers.Interfaces
{
    public interface IUserStorage
    {
        bool HasSessionToken { get; }
        string SessionToken { get; set; }
        bool HasMobileAccessToken { get; }
        string MobileAccessToken { get; set; }
        bool IsLoggedIn { get; set; }
        bool IsConnected { get; set; }
        bool HasUserName { get; }
        string UserName { get; set; }
        UserRole UserRole { get; set; }
        int UserId { get; set; }
        int OrganisationId { get; set; }
    }
}
