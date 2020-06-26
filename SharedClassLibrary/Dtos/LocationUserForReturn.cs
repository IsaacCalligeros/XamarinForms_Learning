using System;
using System.Collections.Generic;
using System.Text;
using SharedClassLibrary.Models;

namespace SharedClassLibrary.Dtos
{
    public class LocationUserForReturn
    {
        public UserForRegisterDto UserDetails { get; set; }
        public int LocationUserId { get; set; }
        public long Permissions { get; set; }
        public Location Location { get; set; }
    }
}
