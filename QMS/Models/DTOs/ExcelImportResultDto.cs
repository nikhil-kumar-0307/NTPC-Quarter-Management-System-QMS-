using System.Collections.Generic;

namespace QMS.Models.DTOs
{
    public class ExcelImportResultDto
    {
        public int TotalRows { get; set; }
        public int Inserted { get; set; }
        public int Updated { get; set; }
        public int Skipped { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}