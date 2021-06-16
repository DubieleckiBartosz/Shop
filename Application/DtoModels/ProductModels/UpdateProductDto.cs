using System.ComponentModel.DataAnnotations;


namespace Application.DtoModels.ProductModels
{
    public class UpdateProductDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        //[Range(0, 35)]
        //public int ProductPoints { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
