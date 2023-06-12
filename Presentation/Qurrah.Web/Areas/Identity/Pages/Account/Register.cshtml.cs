

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Qurrah.Business;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using static Qurrah.Business.Constatnts;

namespace Qurrah.Web.Areas.Identity.Pages.Account

{
    public class RegisterModel : PageModel
    {
        #region Fields
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitofWork;

        //Customization here
        private readonly RoleManager<IdentityRole> _roleManager;

        //End of Customization here

        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        #endregion

        #region Constructors
        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitofWork,
            RoleManager<IdentityRole> roleManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _roleManager = roleManager;
            _unitofWork = unitofWork;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        #endregion

        #region Properties
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "الحقل مطلوب", AllowEmptyStrings = false)]
            [EmailAddress(ErrorMessage = "يجب إدخال بريد إلكتروني صحيح")]
            [Display(Name = "البريد الإلكتروني")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "الحقل مطلوب", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "{0} يجب أن تكون على الأقل {2} وبحد أقصى {1} أحرف", MinimumLength = 6)]
            [DataType(DataType.Password, ErrorMessage = "كلمة المرور يجب أن تحتوي على أحرف من A إلى Z")]
            [Display(Name = "كلمة المرور")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "تأكيد كلمة المرور")]
            [Compare("Password", ErrorMessage = "كلمة المرور غير متطابقة")]
            public string ConfirmPassword { get; set; }

            //Customization here  to add more fields
            [Required(ErrorMessage = "الحقل مطلوب", AllowEmptyStrings = false)]
            [DisplayName("الاسم بالكامل")]
            public string Name { get; set; }

            [DisplayName("المدينة")]
            public string? City { get; set; }

            [DisplayName("الحي")]
            public string? State { get; set; }

            [DisplayName("الرقم البريدي")]
            public string? PostalCode { get; set; }

            [DisplayName("العنوان بالتفصيل")]
            public string? StreetAddress { get; set; }

            [DisplayName("رقم الجوال")]
            [RegularExpression("0[1-9][0-9]{8}", ErrorMessage = "يجب إدخال رقم جوال صحيح")]
            [MaxLength(10)]
            public string? PhoneNumber { get; set; }

            [DisplayName("الدور")]
            public string? Role { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> Roles { get; set; }
            [DisplayName("الشركة")]
            public int? CompanyId { get; set; }
            //[ValidateNever]
            //public IEnumerable<SelectListItem> Companies { get; set; }
            //End of Customization here
        }
        #endregion

        #region Handlers
        public async Task OnGetAsync(string returnUrl = null)
        {
            Input = new InputModel
            {
                Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                }),
                //Companies = _unitofWork.Company.GetAll().Select(c => new SelectListItem
                //{
                //    Text = c.Name,
                //    Value = c.Id.ToString()
                //})
            };
            //End of Customization here

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                //Customiztion here to add more fields
                //user.FullName = Input.Name;
                //user.City = Input.City;
                //user.State = Input.State;
                //user.StreetAddress = Input.StreetAddress;
                //user.PostalCode = Input.PostalCode;
                user.PhoneNumber = Input.PhoneNumber;
                //if (Input.Role == Constants.Role.UserCompany)
                //    user.CompanyId = Input.CompanyId;
                //End of Customization

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //Customization here to assign role to user
                    string roleName = Role.Administrator;
                    //if (!string.IsNullOrWhiteSpace(Input.Role))
                    //    roleName = Input.Role;

                    await _userManager.AddToRoleAsync(user, roleName);
                    //End of Customization

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        //if (User.IsInRole(Constants.Role.Admin))
                        //    TempData["Success"] = "تم إنشاء مستخدم جديد بنجاح.";
                        //else
                            await _signInManager.SignInAsync(user, isPersistent: false);

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        #endregion

        #region Methods
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
        #endregion
    }
}
