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

            UserModel user = new UserModel();
            user.Id = userAuth.Uid;
            user.Username = userData["username"].ToString();
            user.Email = userData["email"].ToString();
            user.FirstName = userData["firstName"].ToString();
            user.LastName = userData["lastName"].ToString();
            user.PhoneNumber = userData["phoneNumber"].ToString();
            user.Address = userData["address"].ToString();
            user.Country = userData["country"].ToString();
            user.Status = userData["status"].ToString();
            user.UrlAvatar = userData["urlAvatar"].ToString();
            user.Status = userData["status"].ToString();

            List<object> chats = userData["chats"] as List<object>;
            user.Chats = chats.Select(x => x.ToString()).ToArray();

            List<object> studies = userData["studies"] as List<object>;
            user.Studies = studies.Select(x => x.ToString()).ToArray();

            List<object> friendRequestR = userData["friendRequestR"] as List<object>;
            user.FriendRequestR = friendRequestR.Select(x => x.ToString()).ToArray();

            List<object> friendRequestS = userData["friendRequestS"] as List<object>;
            user.FriendRequestS = friendRequestS.Select(x => x.ToString()).ToArray();

            List<object> friends = userData["friends"] as List<object>;
            user.Friends = friends.Select(x => x.ToString()).ToArray();

            List<object> nicknames = userData["nicknames"] as List<object>;
            user.Nicknames = nicknames.Select(x => x.ToString()).ToArray();

            user.LastAccess = ((Timestamp)userData["lastAccess"]).ToDateTime();
            user.FirstRegistration = ((Timestamp)userData["firstRegistration"]).ToDateTime();
            
            string userDataJson = JsonSerializer.Serialize(user);
            
            HttpContext.Session.SetString("userDataJson", userDataJson);

            return RedirectToAction("Index", "Home");
        }
    }
}
