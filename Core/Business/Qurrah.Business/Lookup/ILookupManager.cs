namespace Qurrah.Business.Lookup
{
    public interface ILookupManager
    {
        Task<APIResult> GetAllGendersAsync(string culture);
        Task<APIResult> GetAllUserTypesAsync(string culture);
    }
}