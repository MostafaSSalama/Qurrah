using Localization.Services;
using MagicVilla.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Qurrah.Business.EmailService;
using Qurrah.Business.Logging;
using Qurrah.Business.Logging.ILogger;
using Qurrah.Business.Logging.Logger;
using Qurrah.Data;
using Qurrah.Data.Repository;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Integration.ServiceWrappers.Mapping;
using Qurrah.Integration.ServiceWrappers.Services;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using Serilog;
using System.Globalization;
using System.Reflection;

#region Add services to the container.
var builder = WebApplication.CreateBuilder(args);
{
    {
        builder.Services.AddControllersWithViews();

        #region SeriLog
        Log.Logger = new LoggerConfiguration().MinimumLevel
                                              .Error()
                                              .WriteTo
                                              .File("QurrahLogs/Logs.txt", rollingInterval: RollingInterval.Day)
                                              .CreateLogger();
        builder.Host.UseSerilog();

        builder.Services.AddScoped<IErrorLogger, SeriLogger>();
        builder.Services.AddScoped<IInfoLogger, SeriLogger>();
        builder.Services.AddScoped<IExceptionLogging, ExceptionLogging>();
        builder.Services.AddScoped<IInfoLogging, InfoLogging>();

        #endregion

        #region Localization
        //Localiztion - Step 1
        builder.Services.AddSingleton<LanguageService>();

        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
            {
                var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                return factory.Create("SharedResource", assemblyName.Name);
            };
        });

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(culture: "ar-SA", uiCulture: "ar-SA");

            var supportedCultures = new List<CultureInfo> {
                new CultureInfo("ar-SA"),
                new CultureInfo("en-US")
            };
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
        });
        #endregion

        #region Database
        builder.Services.AddDbContext<QurrahDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("QurrahConnectionString")));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddDefaultTokenProviders()
                        .AddEntityFrameworkStores<QurrahDbContext>();

        builder.Services.AddSingleton<IEmailSender, EmailSender>();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";
            options.LogoutPath = "/Identity/Account/Logout";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(100);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        #endregion

        #region AutoMapper
        builder.Services.AddAutoMapper(typeof(MappingConfig));
        #endregion

        #region APIs
        builder.Services.AddHttpClient<IFAQTypeService, FAQTypeService>();
        builder.Services.AddScoped<IFAQTypeService, FAQTypeService>();

        builder.Services.AddHttpClient<IFAQService, FAQService>();
        builder.Services.AddScoped<IFAQService, FAQService>();

        builder.Services.AddScoped<IUserAuthService, UserAuthService>();
        #endregion

        builder.Services.AddHttpContextAccessor();
    }
}
#endregion

#region Configure the HTTP request pipeline.
{
    {
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        //Localiztion - Step 2
        app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

        app.UseRouting();

        //Use Sessions - Step 1
        app.UseSession();

        app.UseAuthentication();

        app.UseAuthorization();
        
        app.MapRazorPages();
        
        app.MapControllerRoute(
            name: "area",
            pattern: "{area=Public}/{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
#endregion