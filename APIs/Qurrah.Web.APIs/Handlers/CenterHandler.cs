using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Utilities;

namespace Qurrah.Web.APIs.Handlers
{
    public class CenterHandler : ICenterHandler
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Properties
        #endregion

        #region Ctor
        public CenterHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<ValidateResult> ValidateCenter(Center center)
        {
            ValidateResult result = new();
            try
            {
                if (!await _unitOfWork.File.AnyAsync(f => f.Id == center.FKIBANFileId))
                    result.ErrorCodes.Add(Constants.Center.IBANFileNotFound);

                if (center.CenterLicenses.IsNullOrEmpty())
                    result.ErrorCodes.Add(Constants.Center.CenterLicensesRequired);
                else
                {
                    var licenseFileIds = center.CenterLicenses.Select(f => f.FKFileId).Distinct().ToList();
                    int count = await _unitOfWork.File.CountAsync(f => licenseFileIds.Contains(f.Id));
                    if (licenseFileIds.IsNullOrEmpty() || licenseFileIds.Count != count)
                        result.ErrorCodes.Add(Constants.Center.LicenseFileNotFound);

                    if (center.CenterLicenses.Any(cl => cl.StartDate >= cl.ExpiryDate))
                        result.ErrorCodes.Add(Constants.Center.EndDateMustbeGreaterThanStartDate);
                }

                if (await _unitOfWork.Center.AnyAsync(c => c.Name.Trim().ToLower().Equals(center.Name.ToLower())))
                    result.ErrorCodes.Add(Constants.Center.CenterNameAlreadyUsed);

                if (!Enum.GetValues<CenterTypeId>().Contains(center.FKCenterTypeId))
                    result.ErrorCodes.Add(Constants.Center.InvalidCenterType);

                var user = await _unitOfWork.ApplicationUser.SingleOrDefaultAsync(u => u.Id.Equals(center.FKCreatedByUserId), tracked: false);
                if (null == user)
                    result.ErrorCodes.Add(Constants.Center.UserNotFound);
                else if (!await _unitOfWork.ApplicationUser.IsUserInRoleAsync(user, Role.Center.ToString()))
                    result.ErrorCodes.Add(Constants.Center.UserNotAllowed);

                if (!IBANUtility.IsValidIban(center.IBAN))
                    result.ErrorCodes.Add(Constants.Center.InvalidIBAN);
            }
            catch
            {
                throw;
            }

            result.IsValid = !result.ErrorCodes.Any();
            return result;
        }
        #endregion
    }
}