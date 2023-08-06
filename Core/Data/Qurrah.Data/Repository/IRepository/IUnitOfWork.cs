namespace Qurrah.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        FAQTypeRepository FAQType { get; }
        FAQRepository FAQ { get; }
        ApplicationUserRepository ApplicationUser { get; }
        LanguageDescriptionRepository LanguageDescription { get; }
        GenderDescriptionRepository GenderDescription { get; }
        UserTypeDescriptionRepository UserTypeDescription { get; }
        CenterTypeDescriptionRepository CenterTypeDescription { get; }
        FileRepository File { get; }
        CenterRepository Center { get; }
        Task SaveAsync();
    }
}