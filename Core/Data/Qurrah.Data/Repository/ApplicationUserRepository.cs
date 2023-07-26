using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Qurrah.Data.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        #region Fields
        private readonly QurrahDbContext _dbContext;
        private UserManager<ApplicationUser> _userManager;
        private readonly string _secretKey;
        #endregion

        #region Ctor
        public ApplicationUserRepository(QurrahDbContext dbContext, UserManager<ApplicationUser> userManager, IConfiguration configuration): base(dbContext)
        {
            _userManager = userManager;
            _secretKey = configuration.GetSection("ApiSettings:LoginSecretKey").Value;
        }
        #endregion

        #region Methods
        public async Task<bool> IsUniqueAsync(string userName, string email)
        {
            bool userExists = await _dbContext.ApplicationUser.AnyAsync(u => u.Email.ToLower().Trim().Equals(email.ToLower().Trim())
                                                                               || u.UserName.ToLower().Trim().Equals(userName.ToLower().Trim()));
            return !userExists;
        }

        public async Task<LoginResult> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _dbContext.ApplicationUser.FirstOrDefaultAsync(u => u.UserName.Trim().ToLower().Equals(loginRequest.UserName.Trim().ToLower())
                                                                                            || u.Email.Trim().ToLower().Equals(loginRequest.UserName.Trim().ToLower()));
            bool isPasswordValid = false;
            if (null != user)
                isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

            if (!isPasswordValid || null == user)
                return new LoginResult(string.Empty, false);

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            string token = WriteToken(user.UserName, role);

            return new LoginResult(token, true);
        }
        public async Task<ApplicationUserRegistrationResult> RegisterWithSaveAsync(ApplicationUser user, string password)
        {
            bool succeeded = false;
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                user.CreatedOn = DateTime.Now;
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    Role role = GetRole(user.FKUserTypeId);
                    await _userManager.AddToRoleAsync(user, role.ToString());
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    succeeded = true;
                }
                else
                {
                    string exceptionMessage = "Register User operation has failed. ";
                    result.Errors?.ToList().ForEach(error => exceptionMessage = $"{exceptionMessage} - {error}");
                    throw new Exception(exceptionMessage);
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw;
            }
            return new ApplicationUserRegistrationResult(succeeded);
        }
        public async Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }
        #endregion

        #region Utilities
        private string WriteToken(string userName, string role)
        {
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var securityKey = new SymmetricSecurityKey(key);

            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddDays(7)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        private Role GetRole(UserTypeId userType)
        {
            switch (userType)
            {
                case UserTypeId.Center:
                    return Role.Center;
                case UserTypeId.CenterApprover:
                    return Role.CenterApprover;
                case UserTypeId.Administrator:
                    return Role.Administrator;
                default:
                    return Role.Parent;
            }
        }
        #endregion
    }
}