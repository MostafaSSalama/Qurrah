using Qurrah.Web.APIs.Models;
using System.Net;

namespace Qurrah.Web.APIs.Utilities
{
    public static class ExceptionUtility
    {
        private const string uniqueExceptionMessage = "cannot insert duplicate key row in object";
        public static APIResponse HandleException(Exception ex)
        {
            string exMessage = ex.Message.ToLower();
            string inExMessage = ex.InnerException?.Message.ToLower();
            if (exMessage.Contains(uniqueExceptionMessage) || inExMessage.Contains(uniqueExceptionMessage))
                return new APIResponse(false, HttpStatusCode.BadRequest, null, new List<string[]> { new string[] { Constants.General.UniquenessViolated } });

            return new APIResponse(false, HttpStatusCode.InternalServerError, null, new List<string[]> { new string[] { ex.Message } });
        }
    }
}