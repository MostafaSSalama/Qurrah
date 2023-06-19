﻿namespace Qurrah.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        FAQTypeRepository FAQType { get; }
        FAQRepository FAQ { get; }
        ParentUserRepository ParentUser { get; }
        CenterUserRepository CenterUser { get; }
        Task SaveAsync();
    }
}