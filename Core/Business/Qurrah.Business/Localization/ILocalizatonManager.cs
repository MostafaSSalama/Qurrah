namespace Qurrah.Business.Localization
{
    public interface ILocalizatonManager
    {
        Task<APIResult> GetLocales(string inCulture);
    }
}