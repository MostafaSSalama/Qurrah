using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Qurrah.Web.Areas.Center.Models;
using Qurrah.Web.Utilities;
using Qurrah.Web.Models;
using Qurrah.Business;
using Qurrah.Business.UserAuth;
using Qurrah.Business.Logging;
using Qurrah.Business.Lookup;
using Qurrah.Business.Extensions;
using Qurrah.Business.File;
using Qurrah.Business.File.Entities;
using Qurrah.Business.Center;
using Qurrah.Business.Localization;
using Qurrah.Integration.ServiceWrappers.DTOs.Center;
using Qurrah.Integration.ServiceWrappers.DTOs.Lookup;
using Qurrah.Integration.ServiceWrappers.DTOs.Localization;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using DTOs = Qurrah.Integration.ServiceWrappers.DTOs.Center;
using FileDTOs = Qurrah.Integration.ServiceWrappers.DTOs.File;

namespace Qurrah.Web.Areas.Center.Controllers
{
    [Area("Center")]
    [Authorize(Roles ="Center")]
    public class ManagementController : Controller
    {
        #region Fields
        private readonly ILocalizedPropertyManager _localizedPropertyManager;
        private readonly ILookupManager _lookupManager;
        private readonly ILocalizationUtility _localizationUtility;
        private readonly IFileManager _fileManager;
        private readonly ICenterManager _centerManager;
        private readonly IMapper _mapper;
        private readonly string[] _allowedFileExtensions;
        private readonly int _allowedFileSize;
        private readonly int _allowedFilesCount;
        private readonly IExceptionLogging _exceptionLogging;
        private readonly LanguageService _localization;
        UserManager<Entities.ApplicationUser> _userManager;
        #endregion

        #region Properties
        public string AuthTokenValue
        {
            get
            {
                return UserManager.JWTTokenValue;
            }
        }
        #endregion

        #region Ctor
        public ManagementController(IMapper mapper, ILocalizedPropertyManager localizedPropertyManager, ILocalizationUtility localizationUtility, ILookupManager lookupManager
                                                  , IExceptionLogging exceptionLogging, IFileManager fileManager, LanguageService localization, IConfiguration configuration
                                                  , UserManager<Entities.ApplicationUser> userManager, ICenterManager centerManager)
        {
            _mapper = mapper;
            _localizedPropertyManager = localizedPropertyManager;
            _localizationUtility = localizationUtility;
            _fileManager = fileManager;
            _centerManager = centerManager;
            _exceptionLogging = exceptionLogging;
            _localization = localization;
            _lookupManager = lookupManager;
            _userManager = userManager;

            _allowedFileExtensions = configuration.GetValue<string>("AppSettings:AllowedFileExtensions")
                                                  .ToLower()
                                                  .Split(',', StringSplitOptions.RemoveEmptyEntries);

            _allowedFileSize = int.Parse(configuration.GetValue<string>("AppSettings:AllowedFileSize")) / (1024 * 1024);
            _allowedFilesCount = int.Parse(configuration.GetValue<string>("AppSettings:AllowedFilesCount"));
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var centerVM = new CenterVM();
            try
            {
                centerVM.Locales = await _localizationUtility.GetLocales();
                var localizedProps = _localizedPropertyManager.PopulateLocalizedPropertyGroups(typeof(DTOs.Center), centerVM.Locales);
                centerVM.LocalizedPropertyGroups = _mapper.Map<List<LocalizedPropertyGroupVM>>(localizedProps);
                centerVM.CenterTypes = await GetAllCenterTypesAsync();
                centerVM.Center.CreatedByUserId = _userManager.GetUserId(User);
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return View(centerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CenterVM centerVM, IFormFile ibanFile, IFormFile licenseFile)
        {
            try
            {
                var ibanFileDetails = new FormFileDetails(ibanFile);
                var licenseFileDetails = new FormFileDetails(licenseFile);
                centerVM.Center.IBANFileId = ibanFileDetails.FileId;
                centerVM.Center.CenterLicenses[0].FileId = licenseFileDetails.FileId;

                if (await TryUploadFiles(ibanFileDetails, licenseFileDetails)
                       && IsCenterLicenseValid(centerVM.Center.CenterLicenses[0])
                       && ModelState.IsValid
                       && await TryCreateCenter(centerVM))
                    return RedirectToAction("Index", "Dashboard");

                centerVM.Locales = await _localizationUtility.GetLocales();
                centerVM.CenterTypes = await GetAllCenterTypesAsync();
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }

            return View(centerVM);
        }
        #endregion

        #region Utilities
        private async Task<List<SelectListItem>> GetAllCenterTypesAsync()
        {
            List<LookupInfo> userTypes = null;
            try
            {
                var result = await _lookupManager.GetAllCenterTypesAsync(Thread.CurrentThread.CurrentCulture.Name);
                if (result.ActionResult == Business.ActionResult.Success)
                    userTypes = result.Result as List<LookupInfo>;
                else
                {
                    if (result?.ActionResult == Business.ActionResult.InternalServerError && result.ErrorMessages?.Any() == true)
                        _exceptionLogging.Log(result.ErrorMessages.Concatenate());
                    else
                        _exceptionLogging.Log("An error occured while loading User Types!");
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return userTypes?.Select(g => new SelectListItem(g.Text, g.Id.ToString()))
                             .ToList() ?? new List<SelectListItem>();
        }

        private async Task<bool> TryUploadFiles(FormFileDetails ibanFileDetails, FormFileDetails licenseFileDetails)
        {
            //Validate files
            bool hasEmptyFile = false;
            if (ibanFileDetails.FileId == Guid.Empty)
            {
                ModelState.AddModelError("", _localization.GetLocalizedString("Validation.Required.Center.IBANFile"));
                hasEmptyFile = true;
            }
            if (licenseFileDetails.FileId == Guid.Empty)
            {
                ModelState.AddModelError("", _localization.GetLocalizedString("Validation.Required.Center.FKFileId"));
                hasEmptyFile = true;
            }

            if (!hasEmptyFile)
            {
                //Upload files
                var result = await _fileManager.UploadMultipleFilesAsync(new List<FileDTOs.FileInfo>
                {
                    ConvertToUploadFileDTO(ibanFileDetails),
                    ConvertToUploadFileDTO(licenseFileDetails)
                });

                if (result?.ActionResult == Business.ActionResult.Success)
                    return true;
                else
                {
                    if ((result?.ActionResult == Business.ActionResult.InternalServerError || result?.ActionResult == Business.ActionResult.BadRequest) && result.ErrorMessages?.Any() == true)
                        foreach (var errorCode in result.ErrorMessages)
                            AddModelStateError(errorCode);
                    else
                    {
                        HttpContext.Session.SetString(Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                        _exceptionLogging.Log("An error occured while uploading center files!");
                    }
                }
            }
            return false;
        }

        private async Task<bool> TryCreateCenter(CenterVM centerVM)
        {
            List<LocalizedProperty> localizedProperties = new();
            centerVM.LocalizedPropertyGroups
                    ?.ForEach(group => localizedProperties.AddRange(group.LocalizedProperties
                                                                         .Where(lp => !string.IsNullOrWhiteSpace(lp.LocaleValue?.Trim()))));
            CenterWithLocalizedProperties centerWithLocalizedProperties = new CenterWithLocalizedProperties
            {
                Center = centerVM.Center,
                LocalizedProperties = localizedProperties
            };

            var apiResult = await _centerManager.CreateAsync(centerWithLocalizedProperties, AuthTokenValue);
            if (apiResult?.ActionResult == Business.ActionResult.Success)
            {
                HttpContext.Session.SetString(Constants.Session_Success, _localization.GetLocalizedString("Messages.SuccessMessages.SaveGeneralSuccess"));
                return true;
            }
            else
            {
                if ((apiResult?.ActionResult == Business.ActionResult.InternalServerError || apiResult?.ActionResult == Business.ActionResult.BadRequest) && apiResult.ErrorMessages?.Any() == true)
                    foreach (var errorCode in apiResult.ErrorMessages)
                        AddModelStateError(errorCode);
                else
                {
                    HttpContext.Session.SetString(Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                    _exceptionLogging.Log("An error occured while saving center!");
                }
            }
            return false;
        }
        private string ConverToBase64String(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            var fileBytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(fileBytes);
            return base64String;
        }

        private FileDTOs.FileInfo ConvertToUploadFileDTO(FormFileDetails details)
        {
            var fileDTO = new FileDTOs.FileInfo();
            fileDTO.Id = details.FileId;
            fileDTO.FileName = Path.GetFileName(details.File.Name);
            fileDTO.FileExtension = Path.GetExtension(details.File.FileName);
            fileDTO.FileData = ConverToBase64String(details.File);
            fileDTO.ContentType = details.File.ContentType;
            return fileDTO;
        }

        private void AddModelStateError(string errorCode)
        {
            switch (errorCode)
            {
                case "File10003":
                    ModelState.AddModelError("", string.Format(_localization.GetLocalizedString("Validation.Custom.Center.InvalidFileExtension"), string.Join(", ", _allowedFileExtensions)));
                    break;
                case "File10005":
                    ModelState.AddModelError("", string.Format(_localization.GetLocalizedString("Validation.Custom.Center.InvalidFileSize"), _allowedFileSize));
                    break;
                case "File10007":
                    ModelState.AddModelError("", string.Format(_localization.GetLocalizedString("Validation.Custom.Center.InvalidFileCount"), string.Concat(_allowedFilesCount)));
                    break;

                case "File10001":
                case "File10002":
                case "File10004":
                case "File10006":
                case "File10008":
                case "File10009":
                    ModelState.AddModelError("", _localization.GetLocalizedString("Validation.Custom.Center.GeneralFileUploadError"));
                    _exceptionLogging.Log($"Something went wrong while uploading files - Error code: {errorCode}");
                    break;


                case "Center10004":
                    ModelState.AddModelError("", string.Format(_localization.GetLocalizedString("Validation.Custom.Center.CenterNameAlreadyUsed"), string.Concat(_allowedFilesCount)));
                    break;
                case "Center10008":
                    ModelState.AddModelError("", string.Format(_localization.GetLocalizedString("Validation.RegularExp.InvalidIBAN"), string.Concat(_allowedFilesCount)));
                    break;

                case "Center10001":
                case "Center10002":
                case "Center10003":
                case "Center10005":
                case "Center10006":
                case "Center10007":
                case "Center10009":
                default:
                    ModelState.AddModelError("", _localization.GetLocalizedString("Validation.Custom.Center.GeneralCreateCenterError"));
                    _exceptionLogging.Log($"Something went wrong while creating center - Error code: {errorCode}");
                    break;
            }
        }

        private bool IsCenterLicenseValid(CenterLicense centerLicense)
        {
            if (centerLicense.ExpiryDate <= centerLicense.StartDate)
            {
                ModelState.AddModelError("", _localization.GetLocalizedString("Validation.Custom.CenterLicense.ExpiryDateLessThanStartDate"));
                return false;
            }
            return true;
        }
        #endregion
    }
}