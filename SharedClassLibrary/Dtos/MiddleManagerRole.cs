using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SharedClassLibrary.Dtos
{
    public class MiddleManagerRole : IdentityRole
    {
        public int UserId { get; set; }
        public Enums.UserRole Role { get; set; }
    }
}
