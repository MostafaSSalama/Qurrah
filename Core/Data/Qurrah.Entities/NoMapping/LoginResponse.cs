using System.ComponentModel.DataAnnotations;

namespace Qurrah.Entities.NoMapping
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