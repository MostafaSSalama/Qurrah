namespace Qurrah.Web.APIs.Models
{
    public class ValidateFilesResult
    {
        public bool IsValid { get; set; }
        public List<ValidateResult> FileResults { get; set; } = new List<ValidateResult>();
    }
}