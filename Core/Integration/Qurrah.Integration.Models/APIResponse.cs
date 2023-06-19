using System.Net;

namespace Qurrah.Integration.Models
{
    public class APIResponse
    {
        public bool IsSuccess { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; }
        public object Result { get; set; }
        public List<string[]> Errors { get; set; }
    }
}