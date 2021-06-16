using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order:BaseEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
      //  public int OrderPoints{ get; set; }//Points
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }        
        public ICollection<OrderLine> Line { get; set; }
      
    }
}
