using Microsoft.EntityFrameworkCore;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class FileRepository : Repository<FileDetails>, IFileRepository
    {
        #region Ctor
        public FileRepository(QurrahDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Methods
        public async Task UploadSingleFileWithSaveAsync(FileDetails file)
        {
            try
            {
                await AddAsync(file);
                await DbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task UploadMultipleFilesWithSaveAsync(IEnumerable<FileDetails> files)
        {
            try
            {
                await AddRangeAsync(files);
                await DbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ActionResult> RemoveSingleFileWithSaveAsync(Guid fileId)
        {
            ActionResult result;
            try
            {
                var file = await SingleOrDefaultAsync(f => f.Id == fileId);
                if (null != file)
                {
                    Remove(file);
                    await DbContext.SaveChangesAsync();
                    result = ActionResult.Success;
                }
                else
                    result = ActionResult.ItemNotFound;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<ActionResult> RemoveMultipleFilesWithSaveAsync(IEnumerable<Guid> fileIds)
        {
            ActionResult result;
            try
            {
                var fileIdsDistinct = fileIds.Distinct();
                var files = await WhereAsync(f => fileIdsDistinct.Contains(f.Id));

                if (files?.Any() == true)
                {
                    RemoveRange(files);
                    await DbContext.SaveChangesAsync();
                    result = ActionResult.Success;
                }
                else
                    result = ActionResult.ItemNotFound;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<DownloadFileResult> DownloadFile(Guid fileId)
        {
            DownloadFileResult result = new();
            try
            {
                var file = await SingleOrDefaultAsync(f => f.Id == fileId);

                result.File = file;
                result.ActionResult = null != file ? ActionResult.Success : ActionResult.ItemNotFound;
            }
            catch
            {
                throw;
            }
            return result;
        }
        #endregion
    }
}