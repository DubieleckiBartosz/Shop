using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
