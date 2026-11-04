using System.ComponentModel.DataAnnotations;

namespace QMS.Models.DTOs
{
    public class QuaterDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Quarter type is required.")]
        [Display(Name = "Quarter Type")]
        public string QuaterType { get; set; }

        [Required(ErrorMessage = "Quarter number is required.")]
        [StringLength(20)]
        [Display(Name = "Quarter Number")]
        public string QuaterNumber { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}