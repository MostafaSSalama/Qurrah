using System.Net;

namespace Qurrah.Web.APIs.Models
{
    public class APIResponse
    {
        public APIResponse(bool isSuccess, HttpStatusCode statusCode, object result)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Result = result;
            Errors = new List<string[]>();
        }
        public APIResponse(bool isSuccess, HttpStatusCode statusCode, object result, List<string[]> errorMessages)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Result = result;
            Errors = errorMessages;
        }
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object Result { get; set; }
        public List<string[]> Errors { get; set; }
    }
}