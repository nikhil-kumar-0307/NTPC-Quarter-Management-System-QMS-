using System.ComponentModel.DataAnnotations;

namespace QMS.Models.DTOs
{
    public class LevelDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Level name is required")]
        [StringLength(20)]
        [Display(Name = "Level Name")]
        public string Name { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}