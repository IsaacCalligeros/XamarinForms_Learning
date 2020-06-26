using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using static SharedClassLibrary.Enums;

namespace SharedClassLibrary.Dtos
{
    public class UserForDetailedDto
    {
        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateofBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime Created { get; set; }
        public int Age { get; set; }
        public int OrganisationId { get; set; }
        public string PasswordResetCode { get; set; }
        public DateTime PasswordResetEmailTimer { get; set; }
        public IEnumerable<LocationUser> LocationUsers { get; set; }

    }
}