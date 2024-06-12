using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace senatinet_asp.Controllers
{
    public class SignUpController : Controller
    {
        FirestoreDb _db;
        FirebaseAuth _auth;
        public SignUpController() 
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "Config/fs_credencials.json");
            _db = FirestoreDb.Create("senatinet-asp");

            _auth = FirebaseAuth.DefaultInstance;
        }
        [HttpPost]
        public ActionResult Index()
        {
            //Aquí se registra el usuario en la db y se inicia sesión
            return View();
        }
    }
}
