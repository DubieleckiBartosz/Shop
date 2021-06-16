using System.ComponentModel.DataAnnotations;


namespace Application.IdentityModels
{
    public class DeleteRoleModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
