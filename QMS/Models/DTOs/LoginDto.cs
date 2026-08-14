using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QMS.Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Employee Number is required")]
        public string EmployeeNumber { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}