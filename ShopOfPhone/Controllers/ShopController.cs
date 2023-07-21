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
        private static ListOfViewModels models = new ListOfViewModels();
        private IUserServices _userServices;
        private IPhoneService _phoneService;
        private IOrderService _orderService;

        public ShopController(IUserServices userServices, 
            IPhoneService phoneService,
            IOrderService orderService)
        {
            _userServices = userServices;
            _phoneService = phoneService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            string str = User.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).ToString();
            string id = str.Remove(0, str.IndexOf(": ") + 2);
             
             var userModels = await _userServices.GetUserById(id);

            if(userModels is not null)
            {
                models.User.Id = userModels.Id;
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

        public async Task<IActionResult> ShoppingCart()
        {
            IEnumerable<OrderViewModel> orderVM = _orderService
                .GetAllFromUser(models.User.Id).Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    Quantity = o.Quantity,
                    Phone = ToPhoneViewModel(_phoneService.GetPhone(o.PhoneId))
                });

            models.Orders = orderVM.ToList();

            if (orderVM is not null)
                return View(models);

            return Content("order is null");
        }

        [HttpGet]
        public async Task<IActionResult> AddOrder(string phoneId, string userId)
        {
            OrderDTO order = new OrderDTO
            {
                Quantity = 1,
                UserId = userId,
                PhoneId = phoneId
            };

            await _orderService.AddFromUserAsync(order);

            return RedirectToAction("Index");
        }


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

        private PhoneViewModel ToPhoneViewModel(PhoneDTO phone)
        {
            return new PhoneViewModel
            {
                Id = phone.Id,
                Name = phone.Name,
                Quantity = phone.Quantity,
                Information = phone.Information,
                PhoneLink = phone.PhotoLink,
                Price = phone.Price,
            };
        }

    }
}
