using System.ComponentModel.DataAnnotations;

namespace QMS.Models.DTOs
{
    public class AgencyDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Agency name is required")]
        [StringLength(100)]
        [Display(Name = "Agency Name")]
        public string AgencyName { get; set; }

        [StringLength(15)]
        public string Contact { get; set; }

        [Required(ErrorMessage = "PO number is required")]
        [StringLength(20)]
        [Display(Name = "PO Number")]
        public string PoNumber { get; set; }

        [Required(ErrorMessage = "Quarter type is required")]
        [Display(Name = "Quarter Type")]
        public string QuarterType { get; set; }

        [Required(ErrorMessage = "Quarter number is required")]
        [StringLength(20)]
        [Display(Name = "Quarter No")]
        public string QuarterNo { get; set; }

        [StringLength(15)]
        [Phone(ErrorMessage = "Enter a valid mobile number")]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }
    }
}