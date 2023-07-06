namespace Qurrah.Entities
{
    public class LoginResponse
    {
        public LoginResponse(string token, bool userExists)
        {
            Token = token;
            UserExists = userExists;
        }
        public string Token { get; set; }
        public bool UserExists { get; set; }
    }
}