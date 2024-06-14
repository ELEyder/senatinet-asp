using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;

namespace senatinet_asp.Services
{
    public class FirebaseService
    {
        private FirestoreDb _db;
        private FirebaseAuth _auth;

        public FirebaseService()
        {
            //Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "Config/fs_credencials.json");
            _db = FirestoreDb.Create("senatinet-asp");
            _auth = FirebaseAuth.DefaultInstance;
        }

        public async Task<FirebaseToken> VerifyIdToken(string idToken)
        {
            return await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
        }
    }
}
