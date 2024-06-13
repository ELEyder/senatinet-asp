using Microsoft.AspNetCore.Mvc;
using senatinet_asp.Models;
using System.Diagnostics;
using senatinet_asp.Attributes;
using System.Text.Json;

namespace senatinet_asp.Controllers
{
    [SessionCheck]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string? userDataJson = HttpContext.Session.GetString("userDataJson");
            if (userDataJson != null)
            {
                UserModel? user = JsonSerializer.Deserialize<UserModel>(userDataJson);
                ViewBag.UserData = user;
                return View();
            }
            else
            {
                ViewBag.UserData = new UserModel();
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
