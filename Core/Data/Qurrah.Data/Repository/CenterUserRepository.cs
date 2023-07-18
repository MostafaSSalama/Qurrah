using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class CenterUserRepository : BaseUserRepository, ICenterUserRepository
    {
        #region Ctor
        public CenterUserRepository(QurrahDbContext dbContext, UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(dbContext, userManager, configuration)
        {
        }
        #endregion

        #region Methods
        public async Task<RegistrationResult> RegisterWithSaveAsync(CenterUserRegistrationRequest registrationRequest)
        {
            bool succeeded = false;
            ApplicationUser user = new();
            user.Email = registrationRequest.CenterOwner.Email;
            user.UserName = registrationRequest.CenterOwner.UserName;
            user.PhoneNumber = registrationRequest.CenterOwner.PhoneNumber;
            user.CreatedOn = DateTime.Now;
            user.FKUserTypeId = UserTypeId.Center;

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequest.CenterOwner.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Role.Center.ToString());
                    _dbContext.CenterOwnerUser.Add(new CenterOwnerUser
                    {
                        FirstName = registrationRequest.CenterOwner.FirstName,
                        SecondName = registrationRequest.CenterOwner.SecondName,
                        ThirdName = registrationRequest.CenterOwner.ThirdName,
                        FourthName = registrationRequest.CenterOwner.FourthName,
                        MobileNumber = registrationRequest.CenterOwner.MobileNumber,
                        FKGenderId = registrationRequest.CenterOwner.FKGenderId,
                        IdNumber = registrationRequest.CenterOwner.IdNumber,
                        FKUserId = user.Id,
                        Center = new Center
                        {
                            Name = registrationRequest.CenterName
                        }
                    });
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    succeeded = true;
                }
                else
                {
                    string exceptionMessage = "Register Center User operation has failed. ";
                    result.Errors?.ToList().ForEach(error => exceptionMessage = $"{exceptionMessage} - {error}");
                    throw new Exception(exceptionMessage);
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw;
            }
            return new RegistrationResult(succeeded);
        }
        #endregion
    }
}