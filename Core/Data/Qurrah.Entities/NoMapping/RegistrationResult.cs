namespace Qurrah.Entities
{
    public class RegistrationResult
    {
        public RegistrationResult(bool succeeded)
        {
            Succeeded = succeeded;
        }
        public bool Succeeded { get; set; }
    }
}