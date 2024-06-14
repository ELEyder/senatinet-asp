using Google.Cloud.Firestore;

namespace senatinet_asp.Models
{
    public class UserModel
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public string? UrlAvatar { get; set; }
        public List<string?> Chats { get; set; }
        public List<string?> Studies { get; set; }
        public List<string?> Works { get; set; }
        public List<string?> FriendRequestR { get; set; }
        public List<string?> FriendRequestS { get; set; }
        public List<string?> Friends { get; set; }
        public List<string?> Nicknames { get; set; }
        public DateTime FirstRegistration { get; set; }
        public DateTime LastAccess { get; set; }

        public UserModel()
        {
            this.Id = "null";
            this.Username = "null";
            this.Email = "null";
            this.FirstName = "null";
            this.LastName = "null";
            this.PhoneNumber = "null";
            this.Address = "null";
            this.Country = "null";
            this.Status = "null";
            this.UrlAvatar = "img/avatars/0.jpg";
            this.Status = "null";
            this.Chats = new List<string?>();
            this.Studies = new List<string?>();
            this.Works = new List<string?>();
            this.FriendRequestR = new List<string?>();
            this.FriendRequestS = new List<string?>();
            this.Friends = new List<string?>();
            this.Nicknames = new List<string?>();
            this.FirstRegistration = DateTime.Now;
            this.LastAccess = DateTime.Now;
        }
        public UserModel(Dictionary<string, object> userData)
        {
            this.Id = userData["id"].ToString();
            this.Username = userData["username"].ToString();
            this.Email = userData["email"].ToString();
            this.FirstName = userData["firstName"].ToString();
            this.LastName = userData["lastName"].ToString();
            this.PhoneNumber = userData["phoneNumber"].ToString();
            this.Address = userData["address"].ToString();
            this.Country = userData["country"].ToString();
            this.Status = userData["status"].ToString();
            this.UrlAvatar = userData["urlAvatar"].ToString();
            this.Status = userData["status"].ToString();
            List<object> chatsTemp = (List<object>)userData["chats"];
            this.Chats = chatsTemp.ConvertAll(obj => obj.ToString());
            List<object> studiesTemp = (List<object>)userData["studies"];
            this.Studies = studiesTemp.ConvertAll(obj => obj.ToString());
            List<object> worksTemp = (List<object>)userData["works"];
            this.Works = worksTemp.ConvertAll(obj => obj.ToString());
            List<object> friendRequestRTemp = (List<object>)userData["friendRequestR"];
            this.FriendRequestR = friendRequestRTemp.ConvertAll(obj => obj.ToString());
            List<object> friendRequestSTemp = (List<object>)userData["friendRequestS"];
            this.FriendRequestS = friendRequestSTemp.ConvertAll(obj => obj.ToString());
            List<object> friendsTemp = (List<object>)userData["friends"];
            this.Friends = friendsTemp.ConvertAll(obj => obj.ToString());
            List<object> nicknamesTemp = (List<object>)userData["nicknames"];
            this.Nicknames = nicknamesTemp.ConvertAll(obj => obj.ToString());
            this.LastAccess = ((Timestamp)userData["lastAccess"]).ToDateTime();
            this.FirstRegistration = ((Timestamp)userData["firstRegistration"]).ToDateTime();
        }
    }
}
