using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMS.Data.Models
{
    [Table("EmployeeMaster")]
    public class EmployeeMaster
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string EmployeeName { get; set; }

        [Required, StringLength(20)]
        public string EmployeeNo { get; set; }

        [Required, StringLength(50)]
        public string Department { get; set; }

        [Required, StringLength(50)]
        public string Designation { get; set; }


        [Required, StringLength(5)]
        public string Level { get; set; }

        [StringLength(100)]
        public string EmailId { get; set; }

        [StringLength(15)]
        public string MobileNo { get; set; }

        [StringLength(15)]
        public string IntercomResidence { get; set; }

        [StringLength(15)]
        public string IntercomOffice { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        // Auto-calculated: DateOfBirth + 60 years
        public DateTime DateOfRetirement { get; set; }

        [StringLength(5)]
        public string BloodGroup { get; set; }

        public string PhotoPath { get; set; }

        [Required, StringLength(20)]
        public string QuarterNo { get; set; }

        [Required, StringLength(5)]
        public string QuarterType { get; set; }   // A, B, C

        [Required, StringLength(20)]
        public string Status { get; set; }        // Active, Empty, Retained, Agency

        public DateTime CreatedAt { get; set; }
    }
}