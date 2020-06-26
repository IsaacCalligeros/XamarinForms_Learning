using System;
using System.Collections.Generic;
using System.Text;

namespace SharedClassLibrary.Dtos
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int OrganisationId { get; set; }
    }
}
