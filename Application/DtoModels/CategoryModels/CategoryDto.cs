using Application.DtoModels.ProductModels;
using System.Collections.Generic;


namespace Application.DtoModels.CategoryModels
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
