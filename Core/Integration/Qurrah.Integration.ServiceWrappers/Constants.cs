namespace Qurrah.Integration.ServiceWrappers;
public static class Constants
{
    public const string Session_AuthTokenName = "token";
    public const string Session_Success = "Success";
    public const string Session_Error = "Error";
    public enum APIType : byte
    {
        HTTPGet,
        HTTPPost,
        HTTPPut,
        HTTPDelete
    }
}