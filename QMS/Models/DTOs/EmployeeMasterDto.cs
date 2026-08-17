using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace QMS.Models.DTOs
{
    public class EmployeeMasterDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee name is required")]
        [StringLength(100)]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Employee number is required")]
        [StringLength(20)]
        [Display(Name = "Employee No")]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [StringLength(50)]
        public string Department { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        [StringLength(50)]
        public string Designation { get; set; }

        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        [StringLength(15)]
        [Phone(ErrorMessage = "Enter a valid mobile number")]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [StringLength(15)]
        [Display(Name = "Intercom No (Residence)")]
        public string IntercomResidence { get; set; }

        [StringLength(15)]
        [Display(Name = "Intercom No (Office)")]
        public string IntercomOffice { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(5)]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "Photo")]
        public HttpPostedFileBase Photo { get; set; }
        public string ExistingPhotoPath { get; set; }

        [Required(ErrorMessage = "Quarter number is required")]
        [StringLength(20)]
        [Display(Name = "Quarter No")]
        public string QuarterNo { get; set; }

        [Required(ErrorMessage = "Quarter type is required")]
        [Display(Name = "Quarter Type")]
        public string QuarterType { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}