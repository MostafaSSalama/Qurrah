namespace Qurrah.Integration.Models;
public static class Constants
{
    public const string Session_AuthTokenName = "token";
    public enum APIType : byte
    {
        HTTPGet,
        HTTPPost,
        HTTPPut,
        HTTPDelete
    }
}