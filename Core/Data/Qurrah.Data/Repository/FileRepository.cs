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
                    FileName = file.FileName.Trim(),
                    FileExtension = file.FileExtension.ToLower().Trim(),
                    FileData = file.FileData.Trim(),
                    FKFileTypeId = (FileTypeId)file.FileTypeId
                });
                await DbContext.SaveChangesAsync();

                result.FileId = fileId;
                result.Succeeded = true;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<UploadMultipleFilesResult> UploadMultipleFilesWithSaveAsync(UploadMultipleFilesRequest files)
        {
            UploadMultipleFilesResult result = new(false, Enumerable.Empty<FileDetails>());
            try
            {
                var fileEntities = files.Files.Select(file => new FileDetails
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName.Trim(),
                    FileExtension = file.FileExtension.ToLower().Trim(),
                    FileData = file.FileData.Trim(),
                    FKFileTypeId = (FileTypeId)file.FileTypeId
                });

                await AddRangeAsync(fileEntities);
                await DbContext.SaveChangesAsync();

                result.Files = fileEntities;
                result.Succeeded = true;
            }
            catch
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
                if (files.Count() == fileIdsDistinct.Count())
                {
                    RemoveRange(files);
                    await DbContext.SaveChangesAsync();

                    result = ActionResult.Success;
                }
                else
                    result = ActionResult.AllOrSomeItemsNotFound;
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