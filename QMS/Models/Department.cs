using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMS.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        public bool IsActive { get; set; } = true;
    }
}