using System.ComponentModel.DataAnnotations;

namespace ShopOfPhone.Models
{
    public class CreateUserViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [Compare("Password")]
        public string? RepeatPassword { get; set; }
    }
}
