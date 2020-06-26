using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedClassLibrary
{
    public static class Enums
    {
        public enum UserRole
        {
            Employee,
            Manager,
            Admin,
            All
        }

        public enum TaskValue
        {
            Photo,
            Bool,
            Text,
            None
        }

        public enum TaskTarget
        {
            User,
            Location
        }

        public enum TaskAccessType
        {
            Owner,
            Assignee,
            Creator
        }
        public enum TrackingStates
        {
            Unmodified = 0,
            Add = 1,
            Update = 2,
            Delete = 3
        }

        public enum OwnerType
        {
            User,
            TaskInstance,
            Location,
        }

        public enum RecurrenceType
        {
            None,
            [Display(Description = "Day")]
            Day,
            [Display(Description = "Week")]
            Week,
            [Display(Description = "Fortnight")]
            Fortnight,
            [Display(Description = "Month")]
            Month,
            [Display(Description = "Year")]
            Year
        }
    }
}
