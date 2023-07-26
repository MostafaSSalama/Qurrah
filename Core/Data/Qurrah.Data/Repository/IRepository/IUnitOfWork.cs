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
        FileRepository File { get; }
        FileTypeRepository FileType { get; }
        CenterRepository Center { get; }
        Task SaveAsync();
    }
}