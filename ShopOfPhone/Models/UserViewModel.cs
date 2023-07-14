namespace ShopOfPhone.Models
{
    public class UserViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public List<CreatePhoneViewModel>? Phones { get; set; }
    }
}
