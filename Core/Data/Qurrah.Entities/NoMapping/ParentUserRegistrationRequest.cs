namespace Qurrah.Entities.NoMapping
{
    public class ParentUserRegistrationRequest
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FourthName { get; set; }
        public string MobileNumber { get; set; }
        public GenderId FKGenderId { get; set; }
        public string IdNumber { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}