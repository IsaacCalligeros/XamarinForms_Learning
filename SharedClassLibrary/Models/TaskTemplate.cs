using SharedClassLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static SharedClassLibrary.Enums;

namespace SharedClassLibrary.Models
{
    public class TaskTemplate : Auditables
    {
        [Key]
        public int TaskTemplateId { get; set; }
        [ForeignKey("OrganisationId")]
        public int OrganisationId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public TaskValue ValueTypeEnum { get; set; }
        public virtual ICollection<TaskInstance> TaskInstances { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
