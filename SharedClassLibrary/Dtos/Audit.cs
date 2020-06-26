using System;
using System.Collections.Generic;
using System.Text;

namespace SharedClassLibrary.Dtos
{
    public class Audit
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
