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

        [HttpGet]
        public IActionResult SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(CreateUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _userServices.CreateUser(model.UserName, model.Password);

                return Redirect("/Shop/Index");
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
                bool result = await _userServices.Authentication(model.UserName, model.Password);

                if (!result)
                    return View(model);

                return Redirect("/Shop/Index");
            }

            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _userServices.SignOut();

            return RedirectToAction("SignIn");
        }
    }
}