using ShopOfPhone.Models;

namespace ShopOfPhone.ViewModels
{
    public class ListOfViewModels
    {
        public UserViewModel User { get; set; } = new UserViewModel();
        public List<PhoneViewModel> Phones { get; set; } = new List<PhoneViewModel>();
        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }
}
