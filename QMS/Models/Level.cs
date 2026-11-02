using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMS.Models
{
    [Table("Levels")]
    public class Level
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Name { get; set; }  

        public bool IsActive { get; set; } = true;
    }
}