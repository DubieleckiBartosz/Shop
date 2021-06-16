using System;
using System.ComponentModel.DataAnnotations;


namespace WebAPI.RequestModels
{
    public class ExcelDetails
    {
        [Required]
        [MaxLength(25)]
        public string FileName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
