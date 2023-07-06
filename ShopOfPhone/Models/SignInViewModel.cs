using System.ComponentModel.DataAnnotations;

namespace ShopOfPhone.Models
{
    public class SignInViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
