namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface ILookupService
    {
        Task<T> GetAllGenders<T>(string culture);
        Task<T> GetAllUserTypes<T>(string culture);
        Task<T> GetAllCenterTypes<T>(string culture);
    }
}