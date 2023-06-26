using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ShopOfPhone.Models;
using System.Diagnostics;

namespace ShopOfPhone.Controllers
{
    public class HomeController : Controller
    {
        private IUserServices _userServices;

        public HomeController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(CreateUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _userServices.CreateUser(model.Email, model.Password);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}