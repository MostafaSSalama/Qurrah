namespace Qurrah.Web.APIs.Models
{
    public class ValidateResult
    {
        public bool IsValid { get; set; }
        public List<string> ErrorCodes { get; set; } = new List<string>();
    }
}