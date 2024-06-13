using Google.Cloud.Firestore;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using senatinet_asp.Models;
using System.Text.Json;

namespace senatinet_asp.Controllers
{
    public class LoginController : Controller
    {
        private FirestoreDb _db;
        private FirebaseAuth _auth;

        public LoginController()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "Config/fs_credencials.json");
            _db = FirestoreDb.Create("senatinet-asp");

            _auth = FirebaseAuth.DefaultInstance;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email)
        {
            UserRecord userAuth = await _auth.GetUserByEmailAsync(email);
            DocumentReference userRef = _db.Collection("users").Document(userAuth.Uid);
            DocumentSnapshot snapshot = await userRef.GetSnapshotAsync();

            Dictionary<string, object> userData = snapshot.ToDictionary();
            userData.Add("id", userAuth.Uid);
            UserModel user = new UserModel(userData);
            
            string userDataJson = JsonSerializer.Serialize(user);
            
            HttpContext.Session.SetString("userDataJson", userDataJson);

            return RedirectToAction("Index", "Home");
        }
    }
}
