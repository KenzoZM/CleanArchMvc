using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.WebUI.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [StringLength(20, ErrorMessage = "the {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Password don't match")]
        public string ConfirmPassword { get; set; }
    }
}
