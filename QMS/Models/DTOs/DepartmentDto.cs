using System.ComponentModel.DataAnnotations;

namespace QMS.Models.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(50)]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [StringLength(10)]
        [Display(Name = "Department Code")]
        public string Code { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}