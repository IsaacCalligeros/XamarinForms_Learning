using System;
using System.Collections.Generic;
using System.Text;
using SharedClassLibrary.Models;

namespace SharedClassLibrary.Dtos
{
    public class TaskInstanceUserDetailsDto
    {
        public TaskInstance TaskInstance { get; set; }
        public UserDetails UserDetails { get; set; }
    }
}
