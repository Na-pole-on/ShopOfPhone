using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entities;

using ShopOfPhone.ViewModels;
using System.Security.Claims;
using System.Security.Principal;

namespace ShopOfPhone.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        ListOfViewModels models = new ListOfViewModels();

        public IActionResult Index()
        {
            //models.User = ReturnUser();
            User u = new User { PhoneNumber = "2039102936" };
            return Content($"{u.PhoneNumber.GetTypeCode()}");
        }

        /*
         private User ReturnUser()
        {
            User user = new User();

            user.UserName = User.Identity.Name;
            
            var email = User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email);
            user.Email = (email is null) ? "NULL" : email.ToString();

            var phone = User.Claims.FirstOrDefault(p => p.Type.ToString() == "IdentityUser<TKey>.PhoneNumber");
            user.PhoneNumber = (phone is null) ? "NULL" : phone.ToString();

            return user;
        }
         
         */
    }
}
