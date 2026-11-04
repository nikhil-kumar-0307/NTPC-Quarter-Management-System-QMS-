using System;
using System.ComponentModel.DataAnnotations;

namespace QMS.Models.DTOs
{
    public class QuaterLogDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Quarter type is required.")]
        [Display(Name = "Quarter Type")]
        public string QuaterType { get; set; }

        [Required(ErrorMessage = "Quarter number is required.")]
        [Display(Name = "Quarter No")]
        public string QuaterNo { get; set; }

        [Required(ErrorMessage = "Please select an employee.")]
        [Display(Name = "Assigned To")]
        public int? AssignedTo { get; set; }

        [Required(ErrorMessage = "Assigned date is required.")]
        [Display(Name = "Assigned Date")]
        public DateTime? AssignedDate { get; set; }

        [Display(Name = "Retained Date")]
        public DateTime? RetainedDate { get; set; }
    }
}