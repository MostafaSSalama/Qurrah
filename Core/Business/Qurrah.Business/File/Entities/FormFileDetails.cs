using Microsoft.AspNetCore.Http;

namespace Qurrah.Business.File.Entities
{
    public class FormFileDetails
    {
        public FormFileDetails(IFormFile file)
        {
            File = file;
            FileId = (null == file || file.Length <= 0) ? Guid.Empty : Guid.NewGuid();
        }
        public Guid FileId { get; private set; }
        public IFormFile File { get; set; }
    }
}