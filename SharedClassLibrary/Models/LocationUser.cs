using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedClassLibrary.Models
{
    public class LocationUser : Auditables
    {
        [Key, Column(Order = 0)]
        public int LocationUserId { get; set; }

        [ForeignKey("LocationId"), Column(Order = 1)]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        [ForeignKey("Id"), Column(Order = 2)]
        public int UserId { get; set; }

        public User User { get; set; }
        public long Permissions { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
