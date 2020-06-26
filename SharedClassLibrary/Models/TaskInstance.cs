using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static SharedClassLibrary.Enums;

namespace SharedClassLibrary.Models
{
    public class TaskInstance : Auditables
    {
        [Key]
        public int TaskInstanceId { get; set; }
        [ForeignKey("TaskTemplateId")]
        [Required]
        public int TaskTemplateId { get; set; }
        public TaskTemplate TaskTemplate { get; set; }
        public string Title { get; set; }
        //Need to check up on this stuff, it's not quite the same but https://entityframeworkcore.com/knowledge-base/53275214/one-to-one-multiple-foreign-key-from-same-table-ef-core
        [ForeignKey("Id")]
        public int? AssignedToId { get; set; }
        public User AssignedTo { get; set; }
        public TaskTarget TargetType { get; set; }
        [ForeignKey("LocationId")]
        public int? LocationId { get; set; }
        public Location Location { get; set; }

        [ForeignKey("OrganisationId")]
        [Required]
        public int OrganisationId { get; set; }

        public bool? BooleanValue { get; set; }
        [ForeignKey("PhotoId")]
        public int? PhotoId { get; set; }
        public string TextValue { get; set; }
        public DateTime? ExpectedCompletionTime { get; set; }
        public DateTime? CompletedTime { get; set; }
        public bool Recurring { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
        public DateTime? RecurringInterval { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }


        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}