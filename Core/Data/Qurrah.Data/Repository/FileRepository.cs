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
        public async Task<UploadSingleFileResult> UploadSingleFileWithSaveAsync(UploadSingleFileRequest file)
        {
            UploadSingleFileResult result = new(false, Guid.Empty);
            try
            {
                Guid fileId = Guid.NewGuid();

                await AddAsync(new FileDetails
                {
                    Id = fileId,
                    FileName = file.FileName,
                    FileData = file.FileData,
                    FKFileTypeId = file.FileType
                });
                await DbContext.SaveChangesAsync();

                result.FileId = fileId;
                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public async Task<UploadMultipleFilesResult> UploadMultipleFilesWithSaveAsync(UploadMultipleFilesRequest files)
        {
            UploadMultipleFilesResult result = new(false, Enumerable.Empty<Guid>());
            try
            {
                var fileEntities = files.Files.Select(file => new FileDetails
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    FileData = file.FileData,
                    FKFileTypeId = file.FileType
                });

                await AddRangeAsync(fileEntities);
                await DbContext.SaveChangesAsync();

                result.FileIds = fileEntities.Select(f => f.Id);
                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
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
            catch (Exception ex)
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
                if (files.Count() == fileIdsDistinct.Count())
                {
                    RemoveRange(files);
                    await DbContext.SaveChangesAsync();

                    result = ActionResult.Success;
                }
                else
                    result = ActionResult.AllOrSomeItemsNotFound;
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        #endregion
    }
}