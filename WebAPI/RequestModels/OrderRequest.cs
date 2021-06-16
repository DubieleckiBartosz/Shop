using System;
using System.ComponentModel.DataAnnotations;


namespace WebAPI.RequestModels
{
    public class OrderRequest
    {
        [Required]
        public DateTime minDate { get; set; }
        public DateTime maxDate { get; set; }
    }
}
