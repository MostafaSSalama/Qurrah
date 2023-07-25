namespace Qurrah.Entities
{
    public class ApplicationUserRegistrationResult
    {
        public ApplicationUserRegistrationResult(bool succeeded)
        {
            Succeeded = succeeded;
        }
        public bool Succeeded { get; set; }
    }
}