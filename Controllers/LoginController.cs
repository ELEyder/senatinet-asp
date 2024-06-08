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
        public async Task<IActionResult> Index(string email, string password)
        {
            UserRecord userAuth = await _auth.GetUserByEmailAsync(email);
            DocumentReference userRef = _db.Collection("users").Document(userAuth.Uid);
            DocumentSnapshot snapshot = await userRef.GetSnapshotAsync();

            Dictionary<string, object> userData = snapshot.ToDictionary();
            List<object> userDataChats = userData["chats"] as List<object>;
            List<object> userDataStudies = userData["studies"] as List<object>;
            List<object> userDataFriendRequestR = userData["friendRequestR"] as List<object>;
            List<object> userDataFriendRequestS = userData["friendRequestS"] as List<object>;
            List<object> userDataFriends = userData["friends"] as List<object>;
            List<object> userDataNicknames = userData["nicknames"] as List<object>;
            string[] chats = userDataChats.Select(x => x.ToString()).ToArray();
            string[] studies = userDataStudies.Select(x => x.ToString()).ToArray();
            string[] friendRequestR = userDataFriendRequestR.Select(x => x.ToString()).ToArray();
            string[] friendRequestS = userDataFriendRequestS.Select(x => x.ToString()).ToArray();
            string[] friends = userDataFriends.Select(x => x.ToString()).ToArray();
            string[] nicknames = userDataNicknames.Select(x => x.ToString()).ToArray();

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
            user.Chats = chats;
            user.Studies = studies;
            user.FriendRequestR = friendRequestR;
            user.FriendRequestS = friendRequestS;
            user.Friends = friends;
            user.Nicknames = nicknames;
            user.LastAccess = ((Timestamp)userData["lastAccess"]).ToDateTime();
            user.FirstRegistration = ((Timestamp)userData["firstRegistration"]).ToDateTime();
            
            string userDataJson = JsonSerializer.Serialize(user);
            
            HttpContext.Session.SetString("userDataJson", userDataJson);

            return RedirectToAction("Index", "Home");
        }
    }
}
