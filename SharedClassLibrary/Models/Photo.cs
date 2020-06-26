using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SharedClassLibrary.Enums;

namespace SharedClassLibrary.Models
{
    public class Photo : Auditables
    {
        [Key]
        public int PhotoId {get; set;}
        public Uri Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public OwnerType OwnerType {get; set;}
        public int OwnerId {get; set; }
        public string PublicId {get; set;}
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}