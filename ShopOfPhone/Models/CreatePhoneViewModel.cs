using BusinessLogicLayer.Dtos;
using Microsoft.Extensions.FileProviders;

namespace ShopOfPhone.Models
{
    public class CreatePhoneViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Information { get; set; }
        public IFormFile? PhonePhoto { get; set; }
    }
}
