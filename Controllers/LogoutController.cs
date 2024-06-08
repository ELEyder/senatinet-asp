using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace senatinet_asp.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Login");
        }
    }
}
