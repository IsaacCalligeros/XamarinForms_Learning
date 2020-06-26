using System;

namespace SharedClassLibrary.Dtos
{
    public class UserForLoginDto
    {

        public UserForLoginDto()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
