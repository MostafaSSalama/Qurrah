using Qurrah.Web.APIs.Models;
using System.Net;

namespace Qurrah.Web.APIs.Utilities
{
    public static class APIUtility
    {
        public static APIResponse HandleException(Exception ex)
        {
            return new APIResponse(false, HttpStatusCode.InternalServerError, null, new List<string[]> { new string[] { ex.ToString() } });
        }
    }
}