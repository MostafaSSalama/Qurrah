namespace Qurrah.Integration.ServiceWrappers.DTOs.Authentication
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public bool UserExists { get; set; }
    }
}