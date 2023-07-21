namespace ShopOfPhone.Models
{
    public class OrderViewModel
    {
        public string? Id { get; set; }
        public int Quantity { get; set; }


        public PhoneViewModel? Phone { get; set; }
        public UserViewModel? User { get; set; }
    }
}
