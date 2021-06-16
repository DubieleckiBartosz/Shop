using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        //[Range(0, 15)]
        //public int ProductPoints { get; set; }//Sprawdź
        public int CategoryId { get; set; }
        public virtual Category Category{ get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }//Dodane
    }
}
