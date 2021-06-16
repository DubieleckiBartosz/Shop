using System.ComponentModel.DataAnnotations;


namespace Application.DtoModels.ProductModels
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
