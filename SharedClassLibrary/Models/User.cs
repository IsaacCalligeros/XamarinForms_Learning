using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using SharedClassLibrary.Dtos;
using static SharedClassLibrary.Enums;

namespace SharedClassLibrary.Models
{
    public class User : IdentityUser<int>, Auditables
    {
        [NotMapped]
        public int UserId {
            get { return Id; }
        }
        public int OrganisationId { get; set; }
        public UserRole UserRole { get; set; }

        //[Required]
        //[MaxLength(256)]
        //public string Username { get; set; }

        public string Gender { get; set; }
        public DateTime DateofBirth { get; set; }
        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(256)]
        public string LastName { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime Created { get; set; }
        public string PasswordResetCode { get; set; }
        public DateTime PasswordResetEmailTimer { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<LocationUser> LocationUsers { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
