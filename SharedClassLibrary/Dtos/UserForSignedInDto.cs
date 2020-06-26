using System;
using System.Collections.Generic;
using System.Text;

namespace SharedClassLibrary.Dtos
{
    public class UserForSignedInDto : UserForDetailedDto
    {
        public string Token { get; set; }
    }
}
