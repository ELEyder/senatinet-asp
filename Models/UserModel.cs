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
        public string[] Chats { get; set; } = [];
        public string[] Studies { get; set; } = [];
        public string[] Works { get; set; } = [];
        public string[] FriendRequestR { get; set; } = [];
        public string[] FriendRequestS { get; set; } = [];
        public string[] Friends { get; set; } = [];
        public string[] Nicknames { get; set; } = [];
        public DateTime FirstRegistration { get; set; }
        public DateTime LastAccess { get; set; }

    }
}
