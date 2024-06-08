using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senatinet_asp.Attributes;

namespace senatinet_asp.Controllers
{
    [SessionCheck]
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
