namespace Qurrah.Integration.ServiceWrappers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LocalizedAttribute : Attribute
    {
        public string LocaleKeyGroup { get; set; }
        public string DisplayValue { get; set; }
        public string LocaleKey { get; set; }
        public bool IsMultiLine { get; set; }
    }
}