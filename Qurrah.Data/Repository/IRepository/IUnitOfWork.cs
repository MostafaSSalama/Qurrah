namespace Qurrah.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        FAQTypeRepository FAQType { get; }
        FAQRepository FAQ { get; }
        Task SaveAsync();
    }
}