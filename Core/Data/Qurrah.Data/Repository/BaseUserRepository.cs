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
    public class BaseUserRepository : IBaseUserRepository
    {
        #region Fields
        protected readonly QurrahDbContext _dbContext;
        protected UserManager<ApplicationUser> _userManager;
        private readonly string _secretKey;
        #endregion

        #region Ctor
        public BaseUserRepository(QurrahDbContext dbContext, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
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

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _dbContext.ApplicationUser.FirstOrDefaultAsync(u => u.UserName.Trim().ToLower().Equals(loginRequest.UserName.Trim().ToLower())
                                                                                            || u.Email.Trim().ToLower().Equals(loginRequest.UserName.Trim().ToLower()));
            bool isPasswordValid = false;
            if (null != user)
                isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

            if (!isPasswordValid || null == user)
                return new LoginResponse(string.Empty, false);

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            string token = WriteToken(user.UserName, role);

            return new LoginResponse(token, true);
        }
        #endregion

        #region Utilities
        protected string WriteToken(string userName, string role)
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
        #endregion
    }
}
