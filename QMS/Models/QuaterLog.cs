using QMS.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMS.Models
{
    public class QuaterLog
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string QuaterType { get; set; }

        [Required, StringLength(20)]
        public string QuaterNo { get; set; }

        // Id of the employee the quarter is assigned to
        public int AssignedTo { get; set; }

        [ForeignKey("AssignedTo")]
        public virtual EmployeeMaster Employee { get; set; }

        public DateTime AssignedDate { get; set; }

        public DateTime? RetainedDate { get; set; }
    }
}