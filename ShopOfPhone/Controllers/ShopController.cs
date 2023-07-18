using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entities;
using ShopOfPhone.ViewModels;
using System.Security.Claims;
using System.Security.Principal;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Dtos;
using ShopOfPhone.Models;
using System.IO;
using DataAccessLayer.Database;
using Microsoft.EntityFrameworkCore;

namespace ShopOfPhone.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        ListOfViewModels models = new ListOfViewModels();
        private IUserServices _userServices;
        private IPhoneService _phoneService;

        public ShopController(IUserServices userServices, 
            IPhoneService phoneService)
        {
            _userServices = userServices;
            _phoneService = phoneService;
        }

        public async Task<IActionResult> Index()
        {
            string str = User.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).ToString();
            string id = str.Remove(0, str.IndexOf(": ") + 2);

            var userModels = await _userServices.GetUserById(id);

            if(userModels is not null)
            {
                models.User.UserName = userModels.UserName;
                models.User.Email = userModels.Email;
                models.User.PhoneNumber = userModels.PhoneNumber;
            }

            List<PhoneDTO> phoneModels = _phoneService.GetPhones().ToList();
            List<PhoneViewModel> phone = ToPhoneViewModel(phoneModels);

            models.Phones = phone;

            return View(models);
        }

        [HttpGet]
        public IActionResult PhoneAdd() => View();

        [HttpPost]
        public async Task<IActionResult> PhoneAdd(CreatePhoneViewModel model)
        {
            if (ModelState.IsValid)
            {
                PhoneDTO phone = new PhoneDTO
                {
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Information = model.Information,
                    Photo = model.PhonePhoto,
                    UserName = User.Identity.Name
                };

                await _phoneService.CreateAsync(phone);

                return Redirect("/Shop/Index");
            }

            return View(model);
        }

        public IActionResult ShoppingCart() => View(models);

        private List<PhoneViewModel> ToPhoneViewModel(List<PhoneDTO> models)
        {
            List<PhoneViewModel> phones = new List<PhoneViewModel>();

            foreach (PhoneDTO phone in models)
            {
                PhoneViewModel model = new PhoneViewModel
                {
                    Id = phone.Id,
                    Name = phone.Name,
                    Quantity = phone.Quantity,
                    Information = phone.Information,
                    Price = phone.Price,
                    PhoneLink = phone.PhotoLink
                };

                phones.Add(model);
            }

            return phones;
        }

    }
}
