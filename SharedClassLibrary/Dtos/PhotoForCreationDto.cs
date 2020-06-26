using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using static SharedClassLibrary.Enums;

namespace SharedClassLibrary.Dtos
{
    public class PhotoForCreationDto
    {
        public string Url { get; set; }
        public Byte[] File { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public OwnerType OwnerType { get; set; }
        public int OwnerId { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public PhotoForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}