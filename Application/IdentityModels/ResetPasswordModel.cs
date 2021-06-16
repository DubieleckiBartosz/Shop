using System.ComponentModel.DataAnnotations;

namespace Application.IdentityModels
{
    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Compare("NewPassword",ErrorMessage ="Passwords are diffrent")]
        [Required]
        public string ConfirmNewPassword { get; set; }
    }
}
