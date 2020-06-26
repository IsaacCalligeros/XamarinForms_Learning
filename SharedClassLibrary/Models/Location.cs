using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedClassLibrary.Models
{
    public class Location : Auditables
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [ForeignKey("OrganisationId")]
        public int OrganisationId { get; set; }
        public virtual ICollection<LocationUser> LocationUsers { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
