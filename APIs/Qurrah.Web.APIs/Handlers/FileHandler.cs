using Microsoft.IdentityModel.Tokens;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.File;
using System.Net;
using System.Text.RegularExpressions;

namespace Qurrah.Web.APIs.Handlers
{
    public class FileHandler : IFileHandler
    {
        #region Fields
        private readonly string[] _allowedFileExtensions;
        private readonly int _allowedFileSize;
        private readonly int _allowedFilesCount;
        private readonly FileTypeId[] _fileTypes;
        #endregion

        #region Properties
        #endregion

        #region Ctor
        public FileHandler(IConfiguration configuration)
        {
            _allowedFileExtensions = configuration.GetValue<string>("ApiSettings:AllowedFileExtensions")
                                                  .ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries);
            _allowedFileSize = int.Parse(configuration.GetValue<string>("ApiSettings:AllowedFileSize"));
            _allowedFilesCount = int.Parse(configuration.GetValue<string>("ApiSettings:AllowedFilesCount"));
            _fileTypes = Enum.GetValues<FileTypeId>();
        }
        #endregion

        #region Methods
        public bool IsBase64String(string base64)
        {
            try
            {
                base64 = base64.Trim();
                byte[] data = Convert.FromBase64String(base64);
                return base64.Length % 4 == 0 && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
            }
            catch
            {
                // If exception is caught, then it is not a base64 encoded string
                return false;
            }
        }

        public bool IsValidFileSize(string base64)
        {
            try
            {
                base64 = base64.Trim();
                byte[] data = Convert.FromBase64String(base64);
                return data.Length <= _allowedFileSize;
            }
            catch
            {
                return false;
            }
        }

        public ValidateResult ValidateFile(UploadFileDTO file)
        {
            ValidateResult result = new();

            //Invalid file name
            if (file.FileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                result.ErrorCodes.Add(Constants.File.InvalidFileName);

            //File extension must start with with dot
            if (!file.FileExtension.StartsWith('.'))
                result.ErrorCodes.Add(Constants.File.ExtensionStartWithDot);

            //File extension can only contain dot at the beginning
            if (file.FileExtension.LastIndexOf('.') > 0)
                result.ErrorCodes.Add(Constants.File.ExtensionDotAtBeginning);

            //Invalid file extension. Allowed extensions are .pdf,.docx,.doc,.png,.jpg,.jpeg
            if (!_allowedFileExtensions.Contains(file.FileExtension.ToLower().Trim()))
                result.ErrorCodes.Add(Constants.File.InvalidExtension);

            //Invalid base64
            if (!IsBase64String(file.FileData))
                result.ErrorCodes.Add(Constants.File.InvalidBase64);

            //Invalid file size. Allowed size is 1MB
            if (!IsValidFileSize(file.FileData))
                result.ErrorCodes.Add(Constants.File.InvalidFileSize);

            //Invalid file type
            if (!_fileTypes.Contains((FileTypeId)file.FileTypeId))
                result.ErrorCodes.Add(Constants.File.InvalidFileType);

            result.IsValid = !result.ErrorCodes.Any();
            return result;
        }

        public ValidateFilesResult ValidateFiles(UploadMultipleFilesDTO files)
        {
            ValidateFilesResult result = new ValidateFilesResult();

            //Empty files list
            if (files.Files.IsNullOrEmpty())
                result.FileResults.Add(new ValidateResult
                {
                    IsValid = false,
                    ErrorCodes = new List<string>
                    {
                        Constants.File.EmptyFilesList
                    }
                });
            //Invalid file 
            else if (files.Files.Count() > _allowedFilesCount)
                result.FileResults.Add(new ValidateResult
                {
                    IsValid = false,
                    ErrorCodes = new List<string>
                    {
                        Constants.File.InvalidFilesCount
                    }
                });
            else
                foreach (var file in files.Files)
                {
                    ValidateResult fileResult = ValidateFile(file);
                    result.FileResults.Add(fileResult);
                }

            result.IsValid = result.FileResults.All(fr => fr.IsValid);
            return result;
        }
        #endregion
    }
}