using Google.Cloud.Firestore;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using senatinet_asp.Models;
using System.Text.Json;
using senatinet_asp.Services;

namespace senatinet_asp.Controllers
{
    public class LoginController : Controller
    {
        private FirestoreDb _db;
        private FirebaseAuth _auth;
        private FirebaseService firebaseService;

        public LoginController()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "Config/fs_credencials.json");
            _db = FirestoreDb.Create("senatinet-asp");
            _auth = FirebaseAuth.DefaultInstance;
            firebaseService = new FirebaseService();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate()
        {

            string idToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            FirebaseToken decodedToken = await firebaseService.VerifyIdToken(idToken);
            string uid = decodedToken.Uid;

            DocumentReference userRef = _db.Collection("users").Document(uid);
            DocumentSnapshot snapshot = await userRef.GetSnapshotAsync();

            Dictionary<string, object> userData = snapshot.ToDictionary();

            UserModel user;

            if (userData == null)
            {
                user = new UserModel();
            } else
            {
                userData.Add("id", uid);
                user = new UserModel(userData);
            }

            string userDataJson = JsonSerializer.Serialize(user);

            HttpContext.Session.SetString("userDataJson", userDataJson);
            HttpContext.Session.SetString("token", idToken);

            return RedirectToAction("Index", "Home");

        }
    }
}
