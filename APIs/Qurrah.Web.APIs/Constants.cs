namespace Qurrah.Web.APIs
{
    public static class Constants
    {
        public static class File
        {
            public static readonly string ExtensionStartWithDot = "File10001";
            public static readonly string ExtensionDotAtBeginning = "File10002";
            public static readonly string InvalidExtension = "File10003";
            public static readonly string InvalidBase64 = "File10004";
            public static readonly string InvalidFileSize = "File10005";
            public static readonly string InvalidFileName = "File10006";
            public static readonly string InvalidFilesCount = "File10007";
            public static readonly string EmptyFilesList = "File10008";
            public static readonly string AllOrSomeItemsNotFound = "File10009";
        }
        public static class Center
        {
            public static readonly string IBANFileNotFound = "Center10001";
            public static readonly string CenterLicensesRequired = "Center10002";
            public static readonly string LicenseFileNotFound = "Center10003";
            public static readonly string CenterNameAlreadyUsed = "Center10004";
            public static readonly string InvalidCenterType = "Center10005";
            public static readonly string UserNotFound = "Center10006";
            public static readonly string UserNotAllowed = "Center10007";
            public static readonly string InvalidIBAN = "Center10008";
            public static readonly string EndDateMustbeGreaterThanStartDate = "Center10009";
        }
        public static class IBAN
        {
            public static readonly string BankCodes = "50,15,30,60,76,85,55,81,95,90,05,71,75,82,10,20,80,45,40,83,65,84";
        }
        public static class General
        {
            public static readonly string UniquenessViolated = "General10001";
        }
    }
}