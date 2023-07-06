namespace Qurrah.Integration.ServiceWrappers.DTOs.Authentication
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public bool UserExists { get; set; }
    }
}