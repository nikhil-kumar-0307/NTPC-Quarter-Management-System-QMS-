using System;
using System.ComponentModel.DataAnnotations;

namespace QMS.Data.Models
{
    public class Agency
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string AgencyName { get; set; }

        [StringLength(15)]
        public string Contact { get; set; }

        [Required, StringLength(20)]
        public string PoNumber { get; set; }

        [Required, StringLength(5)]
        public string QuarterType { get; set; }   // A, B, C

        [Required, StringLength(20)]
        public string QuarterNo { get; set; }

        [StringLength(15)]
        public string MobileNo { get; set; }

        [StringLength(100)]
        public string EmailId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}