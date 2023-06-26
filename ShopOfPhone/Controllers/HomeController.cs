using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ShopOfPhone.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}