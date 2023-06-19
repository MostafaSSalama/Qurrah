using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Entities.NoMapping;

namespace Qurrah.Data.Repository
{
    public class ParentUserRepository : BaseUserRepository, IParentUserRepository
    {
        #region Fields
        private readonly QurrahDbContext _dbContext;
        private UserManager<ApplicationUser> _userManager;
        #endregion

        #region Ctor
        public ParentUserRepository(QurrahDbContext dbContext, UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(dbContext, userManager, configuration)
        {
        }
        #endregion

        #region Methods
        public async Task<RegistrationResponse> RegisterWithSaveAsync(ParentUserRegistrationRequest registrationRequest)
        {
            bool succeeded = false;
            ApplicationUser user = new();
            user.Email = registrationRequest.Email;
            user.UserName = registrationRequest.UserName;
            user.PhoneNumber = registrationRequest.PhoneNumber;
            user.CreatedOn = DateTime.Now;
            user.FKUserTypeId = UserTypeId.Parent;

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequest.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Role.Parent.ToString());
                    _dbContext.ParentUser.Add(new ParentUser
                    {
                        FirstName = registrationRequest.FirstName,
                        SecondName = registrationRequest.SecondName,
                        ThirdName = registrationRequest.ThirdName,
                        FourthName = registrationRequest.FourthName,
                        MobileNumber = registrationRequest.MobileNumber,
                        FKGenderId = registrationRequest.FKGenderId,
                        IdNumber = registrationRequest.IdNumber,
                        FKUserId = user.Id
                    });
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    succeeded = true;
                }
                else
                {
                    string exceptionMessage = "Register Parent User operation has failed. ";
                    result.Errors?.ToList().ForEach(error => exceptionMessage = $"{exceptionMessage} - {error}");
                    throw new Exception(exceptionMessage);
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw;
            }
            return new RegistrationResponse(succeeded);
        }
        #endregion
    }
}