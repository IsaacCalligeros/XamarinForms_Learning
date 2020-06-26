using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedClassLibrary.Models
{
    public class Organisation : Auditables
    {
        [Key]
        public int OrganisationId { get; set; }
        public string Name { get; set; }
        public int Settings { get; set; }

        [Required]
        [MaxLength(5)]
        public string Code { get; set; }
        [Required]
        [MaxLength(10)]
        public string Key { get; set; }
        public DateTime Expiry { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<TaskTemplate> TaskTemplates { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
