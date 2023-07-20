namespace Qurrah.Web.APIs.Models
{
    public class ValidateFileResult
    {
        public bool IsValid { get; set; }
        public List<string> ErrorCodes { get; set; } = new List<string>();
    }
}