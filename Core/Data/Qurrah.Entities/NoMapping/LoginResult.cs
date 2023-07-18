namespace Qurrah.Entities
{
    public class LoginResult
    {
        public LoginResult(string token, bool userExists)
        {
            Token = token;
            UserExists = userExists;
        }
        public string Token { get; set; }
        public bool UserExists { get; set; }
    }
}