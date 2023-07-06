using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(CreateUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _userServices.CreateUser(model.UserName, model.Password);

                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult SignIn() => View();

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userServices.Authentication(model.UserName, model.Password);

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}