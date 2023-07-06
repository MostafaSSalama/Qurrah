namespace Qurrah.Entities
{
    public class RegistrationResponse
    {
        public RegistrationResponse(bool succeeded)
        {
            Succeeded = succeeded;
        }
        public bool Succeeded { get; set; }
    }
}