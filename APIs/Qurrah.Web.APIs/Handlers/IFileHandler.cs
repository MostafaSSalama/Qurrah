using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.File;

namespace Qurrah.Web.APIs.Handlers
{
    public interface IFileHandler
    {
        bool IsBase64String(string base64);
        bool IsValidFileSize(string base64);
        ValidateResult ValidateFile(UploadFileDTO file);
        ValidateFilesResult ValidateFiles(UploadMultipleFilesDTO files);
    }
}