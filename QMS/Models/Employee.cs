using System;
using System.ComponentModel.DataAnnotations;

namespace QMS.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(20)]
        public string EmployeeNo { get; set; }

        [Required, StringLength(20)]
        public string QuarterNo { get; set; }

        [Required, StringLength(5)]
        public string QuarterType { get; set; }   // A, B, C

        [Required, StringLength(20)]
        public string Status { get; set; }        // Active, Empty, Retained, Agency

        public string ProfilePicPath { get; set; }

        [StringLength(15)]
        public string ResidenceTelNo { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}