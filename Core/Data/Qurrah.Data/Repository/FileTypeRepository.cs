using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class FileTypeRepository : Repository<FileType>
    {
        public FileTypeRepository(QurrahDbContext dbContext) : base(dbContext)
        {

        }
        
        public override void Update(FileType fileType)
        {
            base.Update(fileType);
        }
    }
}