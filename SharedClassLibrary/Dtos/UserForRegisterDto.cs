using System;
using System.ComponentModel.DataAnnotations;
using static SharedClassLibrary.Enums;

namespace SharedClassLibrary.Dtos
{
    public class UserForRegisterDto {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "You must specify a password between 20 and 6 characters")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public UserRole Role { get; set; }
        public int OrganisationId { get; set; }

    }
}