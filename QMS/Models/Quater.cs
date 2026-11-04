using System.ComponentModel.DataAnnotations;

namespace QMS.Models
{
    public class Quater
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string QuaterType { get; set; }

        [Required, StringLength(20)]
        public string QuaterNumber { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; }

        public bool IsActive { get; set; } = true;
    }
}