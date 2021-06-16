
using System.ComponentModel.DataAnnotations;


namespace Application.DtoModels.CategoryModels
{
    public class UpdateCategoryDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
