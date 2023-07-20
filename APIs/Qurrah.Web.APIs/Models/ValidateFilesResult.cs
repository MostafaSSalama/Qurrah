namespace Qurrah.Web.APIs.Models
{
    public class ValidateFilesResult
    {
        public bool IsValid { get; set; }
        public List<ValidateFileResult> FileResults { get; set; } = new List<ValidateFileResult>();
    }
}