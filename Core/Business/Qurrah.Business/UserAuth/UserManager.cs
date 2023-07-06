using Microsoft.AspNetCore.Http;

namespace Qurrah.Business.UserAuth
{
    public static class UserManager
    {
        public static string JWTTokenValue
        {
            get
            {
                HttpContextAccessor ctxAccessor = new HttpContextAccessor();
                ctxAccessor.HttpContext.Request.Cookies.TryGetValue(Constants.JWTTokenName, out string token);
                return token;
            }
        }

        public static bool IsJWTTokenValid(out string token)
        {
            HttpContextAccessor ctxAccessor = new HttpContextAccessor();
            ctxAccessor.HttpContext.Request.Cookies.TryGetValue(Constants.JWTTokenName, out token);
            return !string.IsNullOrWhiteSpace(token);
        }
    }
}