using System.ComponentModel.DataAnnotations;
using System.Web;

namespace QMS.Models.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employee number is required")]
        [StringLength(20)]
        [Display(Name = "Employee No")]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "Quarter number is required")]
        [StringLength(20)]
        [Display(Name = "Quarter No")]
        public string QuarterNo { get; set; }

        [Required(ErrorMessage = "Quarter type is required")]
        [Display(Name = "Quarter Type")]
        public string QuarterType { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Display(Name = "Residence Telephone No")]
        [StringLength(15)]
        [Phone(ErrorMessage = "Enter a valid phone number")]
        public string ResidenceTelNo { get; set; }

        [Display(Name = "Profile Picture")]
        public HttpPostedFileBase ProfilePic { get; set; }

        public string ExistingProfilePicPath { get; set; }
    }
}