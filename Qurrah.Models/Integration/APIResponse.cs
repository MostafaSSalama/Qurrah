using System.Net;

namespace Qurrah.Models.Integration
{
    public class APIResponse
    {
        public bool IsSuccess { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; }
        public object Result { get; set; }
        public List<string[]> Errors { get; set; }
    }
}