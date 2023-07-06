namespace Qurrah.Business
{
    public static class Constants
    {
        public const string JWTTokenName = "jToken";

        public const string Session_Success = "Success";
        public const string Session_Error = "Error";

        public static class Role
        {
            public const string Administrator = "Administrator";
            public const string CenterApprover = "CenterApprover";
            public const string Parent = "Parent";
            public const string Center = "Center";
        }
    }
}