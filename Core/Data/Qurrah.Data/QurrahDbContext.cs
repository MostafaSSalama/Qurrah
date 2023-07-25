using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Qurrah.Entities;
using Microsoft.AspNetCore.Identity;

namespace Qurrah.Data
{
    public class QurrahDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Ctor
        public QurrahDbContext(DbContextOptions options) : base(options)
        {

        }
        #endregion

        #region DbSets
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<FAQType> FAQType { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<GenderDescription> GenderDescription { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<LanguageDescription> LanguageDescription { get; set; }
        public DbSet<LocalizedProperty> LocalizedProperty { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<UserTypeDescription> UserTypeDescription { get; set; }
        public DbSet<CenterType> CenterType { get; set; }
        public DbSet<CenterTypeDescription> CenterTypeDescription { get; set; }
        public DbSet<CenterStatus> CenterStatus { get; set; }
        public DbSet<CenterStatusDescription> CenterStatusDescription { get; set; }
        public DbSet<Center> Center { get; set; }
        public DbSet<CenterLicenseStatus> CenterLicenseStatus { get; set; }
        public DbSet<CenterLicense> CenterLicense { get; set; }
        public DbSet<CenterLicenseStatusDescription> CenterLicenseStatusDescription { get; set; }
        public DbSet<FileType> FileType { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Enums
            modelBuilder.Entity<Gender>()
                       .HasIndex(l => l.Name)
                       .IsUnique();

            modelBuilder.Entity<Gender>()
                        .HasData(Enum.GetValues(typeof(GenderId))
                                     .Cast<GenderId>()
                                     .Select(e => new Gender()
                                     {
                                         Id = e,
                                         Name = e.ToString()
                                     }));

            modelBuilder.Entity<UserType>()
                       .HasIndex(l => l.Name)
                       .IsUnique();

            modelBuilder.Entity<UserType>()
                        .HasData(Enum.GetValues(typeof(UserTypeId))
                                     .Cast<UserTypeId>()
                                     .Select(e => new UserType()
                                     {
                                         Id = e,
                                         Name = e.ToString()
                                     }));

            modelBuilder.Entity<Language>()
                        .HasIndex(l => l.LanguageCulture)
                        .IsUnique();

            modelBuilder.Entity<Language>()
                        .HasData(new List<Language>{
                                     new Language()
                                     {
                                         Id = LanguageId.Arabic,
                                         Name = LanguageId.Arabic.ToString(),
                                         DisplayOrder = 1,
                                         LanguageCulture = "ar-SA",
                                         Published = true,
                                         RTL = true
                                     },
                                     new Language()
                                     {
                                         Id = LanguageId.English,
                                         Name = LanguageId.English.ToString(),
                                         DisplayOrder = 2,
                                         LanguageCulture = "en-US",
                                         Published = true,
                                         RTL = false
                                     }
                              });


            modelBuilder.Entity<FileType>()
                        .HasIndex(l => l.Name)
                        .IsUnique();

            modelBuilder.Entity<FileType>()
                        .HasData(new List<FileType>
                        {
                            new FileType()
                            {
                                Id = FileTypeId.PDF,
                                Name = FileTypeId.PDF.ToString(),
                                ContentType = "application/pdf"
                            },
                            new FileType()
                            {
                                Id = FileTypeId.DOCX,
                                Name = FileTypeId.DOCX.ToString(),
                                ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                            },
                            new FileType()
                            {
                                Id = FileTypeId.DOC,
                                Name = FileTypeId.DOC.ToString(),
                                ContentType = "application/msword"
                            },
                            new FileType()
                            {
                                Id = FileTypeId.PNG,
                                Name = FileTypeId.PNG.ToString(),
                                ContentType = "image/x-png"
                            },
                            new FileType()
                            {
                                Id = FileTypeId.JPG,
                                Name = FileTypeId.JPG.ToString(),
                                ContentType = "image/jpeg"
                            },
                            new FileType()
                            {
                                Id = FileTypeId.JPEG,
                                Name = FileTypeId.JPEG.ToString(),
                                ContentType = "image/jpeg"
                            }
                        });

            modelBuilder.Entity<CenterLicenseStatus>()
                        .HasIndex(l => l.Name)
                        .IsUnique();

            modelBuilder.Entity<CenterLicenseStatus>()
                        .HasData(Enum.GetValues(typeof(CenterLicenseStatusId))
                                     .Cast<CenterLicenseStatusId>()
                                     .Select(e => new CenterLicenseStatus()
                                     {
                                         Id = e,
                                         Name = e.ToString()
                                     }));

            modelBuilder.Entity<CenterType>()
                        .HasIndex(l => l.Name)
                        .IsUnique();

            modelBuilder.Entity<CenterType>()
                        .HasData(Enum.GetValues(typeof(CenterTypeId))
                                     .Cast<CenterTypeId>()
                                     .Select(e => new CenterType()
                                     {
                                         Id = e,
                                         Name = e.ToString()
                                     }));

            modelBuilder.Entity<CenterStatus>()
                        .HasIndex(l => l.Name)
                        .IsUnique();

            modelBuilder.Entity<CenterStatus>()
                        .HasData(Enum.GetValues(typeof(CenterStatusId))
                                     .Cast<CenterStatusId>()
                                     .Select(e => new CenterStatus()
                                     {
                                         Id = e,
                                         Name = e.ToString()
                                     }));
            #endregion

            #region GenderDescription
            modelBuilder.Entity<GenderDescription>()
                        .Property(e => e.FKLanguageId)
                        .HasConversion<int>();

            modelBuilder.Entity<GenderDescription>()
                        .Property(e => e.FKLanguageId)
                        .HasConversion<int>();

            modelBuilder.Entity<GenderDescription>()
                        .HasIndex(l => new
                        {
                            l.FKLanguageId,
                            l.FKGenderId
                        })
                        .IsUnique();

            modelBuilder.Entity<GenderDescription>().HasData(new List<GenderDescription>
            {
                new GenderDescription
                {
                    Id = 1,
                    FKGenderId = GenderId.Male,
                    FKLanguageId = LanguageId.Arabic,
                    Description = "ذكر"
                },
                new GenderDescription
                {
                    Id = 2,
                    FKGenderId = GenderId.Male,
                    FKLanguageId = LanguageId.English,
                    Description = "Male"
                },
                new GenderDescription
                {
                    Id = 3,
                    FKGenderId = GenderId.Female,
                    FKLanguageId = LanguageId.Arabic,
                    Description = "أنثى"
                },
                new GenderDescription
                {
                    Id = 4,
                    FKGenderId = GenderId.Female,
                    FKLanguageId = LanguageId.English,
                    Description = "Female"
                }
            });
            #endregion

            #region GenderDescription
            modelBuilder.Entity<UserTypeDescription>()
                        .Property(e => e.FKLanguageId)
                        .HasConversion<int>();

            modelBuilder.Entity<UserTypeDescription>()
                        .Property(e => e.FKLanguageId)
                        .HasConversion<int>();

            modelBuilder.Entity<UserTypeDescription>()
                        .HasIndex(l => new
                        {
                            l.FKLanguageId,
                            l.FKUserTypeId
                        })
                        .IsUnique();

            modelBuilder.Entity<UserTypeDescription>().HasData(new List<UserTypeDescription>
            {
                new UserTypeDescription
                {
                    Id = 1,
                    FKUserTypeId = UserTypeId.Administrator,
                    FKLanguageId = LanguageId.Arabic,
                    Description = "مدير النظام"
                },
                new UserTypeDescription
                {
                    Id = 2,
                    FKUserTypeId = UserTypeId.Administrator,
                    FKLanguageId = LanguageId.English,
                    Description = "Administrator"
                },
                new UserTypeDescription
                {
                    Id = 3,
                    FKUserTypeId = UserTypeId.Parent,
                    FKLanguageId = LanguageId.Arabic,
                    Description = "ولي أمر"
                },
                new UserTypeDescription
                {
                    Id = 4,
                    FKUserTypeId = UserTypeId.Parent,
                    FKLanguageId = LanguageId.English,
                    Description = "Parent"
                },
                new UserTypeDescription
                {
                    Id = 5,
                    FKUserTypeId = UserTypeId.Center,
                    FKLanguageId = LanguageId.Arabic,
                    Description = "مركز"
                },
                new UserTypeDescription
                {
                    Id = 6,
                    FKUserTypeId = UserTypeId.Center,
                    FKLanguageId = LanguageId.English,
                    Description = "Center"
                },
                new UserTypeDescription
                {
                    Id = 7,
                    FKUserTypeId = UserTypeId.CenterApprover,
                    FKLanguageId = LanguageId.Arabic,
                    Description = "مراجع مراكز"
                },
                new UserTypeDescription
                {
                    Id = 8,
                    FKUserTypeId = UserTypeId.CenterApprover,
                    FKLanguageId = LanguageId.English,
                    Description = "Center Approver"
                }
            });
            #endregion

            #region LanguageDescription
            modelBuilder.Entity<LanguageDescription>()
                        .Property(e => e.FKLanguageId)
                        .HasConversion<int>();

            modelBuilder.Entity<LanguageDescription>()
                        .HasOne(ld => ld.Language)
                        .WithMany(l => l.LanguageDescriptions)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LanguageDescription>()
                        .HasOne(ld => ld.InLanguage)
                        .WithMany(l => l.InLanguageDescriptions)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LanguageDescription>()
                        .HasIndex(l => new
                        {
                            l.FKLanguageId,
                            l.FKInLanguageId
                        })
                        .IsUnique();

            modelBuilder.Entity<LanguageDescription>()
                        .HasData(new List<LanguageDescription>
                        {
                            new LanguageDescription
                            {
                                Id = 1,
                                FKLanguageId = LanguageId.Arabic,
                                FKInLanguageId = LanguageId.Arabic,
                                Description = "العربية"
                            },
                            new LanguageDescription
                            {
                                Id = 2,
                                FKLanguageId = LanguageId.Arabic,
                                FKInLanguageId = LanguageId.English,
                                Description = "Arabic"
                            },
                            new LanguageDescription
                            {
                                Id = 3,
                                FKLanguageId = LanguageId.English,
                                FKInLanguageId = LanguageId.Arabic,
                                Description = "الإنجليزية"
                            },
                            new LanguageDescription
                            {
                                Id = 4,
                                FKLanguageId = LanguageId.English,
                                FKInLanguageId = LanguageId.English,
                                Description = "English"
                            }
                        });
            #endregion

            #region CenterLicenseStatusDescription
            modelBuilder.Entity<CenterLicenseStatusDescription>()
                        .Property(cld => cld.FKStatusId)
                        .HasConversion<int>();

            modelBuilder.Entity<CenterLicenseStatusDescription>()
                        .Property(cld => cld.FKLanguageId)
                        .HasConversion<int>();

            modelBuilder.Entity<CenterLicenseStatusDescription>()
                        .HasIndex(l => new
                        {
                            l.FKLanguageId,
                            l.FKStatusId
                        })
                        .IsUnique();

            modelBuilder.Entity<CenterLicenseStatusDescription>()
                        .HasData(new List<CenterLicenseStatusDescription>
                        {
                            new CenterLicenseStatusDescription
                            {
                                Id = 1,
                                FKLanguageId = LanguageId.English,
                                FKStatusId = CenterLicenseStatusId.UnderConsideration,
                                Description = "Under Consideration"
                            },
                            new CenterLicenseStatusDescription
                            {
                                Id = 2,
                                FKLanguageId = LanguageId.Arabic,
                                FKStatusId = CenterLicenseStatusId.UnderConsideration,
                                Description = "قيد الدراسة"
                            },
                            new CenterLicenseStatusDescription
                            {
                                Id = 3,
                                FKLanguageId = LanguageId.English,
                                FKStatusId = CenterLicenseStatusId.Approved,
                                Description = "Approved"
                            },
                            new CenterLicenseStatusDescription
                            {
                                Id = 4,
                                FKLanguageId = LanguageId.Arabic,
                                FKStatusId = CenterLicenseStatusId.Approved,
                                Description = "مقبول"
                            },
                            new CenterLicenseStatusDescription
                            {
                                Id = 5,
                                FKLanguageId = LanguageId.English,
                                FKStatusId = CenterLicenseStatusId.Rejected,
                                Description = "Rejected"
                            },
                            new CenterLicenseStatusDescription
                            {
                                Id = 6,
                                FKLanguageId = LanguageId.Arabic,
                                FKStatusId = CenterLicenseStatusId.Rejected,
                                Description = "مرفوض"
                            }
                        });
            #endregion

            #region CenterTypeDescription
            modelBuilder.Entity<CenterTypeDescription>()
                        .Property(ctd => ctd.FKCenterTypeId)
                        .HasConversion<int>();

            modelBuilder.Entity<CenterTypeDescription>()
                        .Property(ctd => ctd.FKLanguageId)
                        .HasConversion<int>();

            modelBuilder.Entity<CenterTypeDescription>()
                        .HasIndex(l => new
                        {
                            l.FKLanguageId,
                            l.FKCenterTypeId
                        })
                        .IsUnique();

            modelBuilder.Entity<CenterTypeDescription>()
                        .HasData(new List<CenterTypeDescription>
                        {
                            new CenterTypeDescription
                            {
                                Id = 1,
                                FKLanguageId = LanguageId.English,
                                FKCenterTypeId = CenterTypeId.InDependent,
                                Description = "InDependent"
                            },
                            new CenterTypeDescription
                            {
                                Id = 2,
                                FKLanguageId = LanguageId.Arabic,
                                FKCenterTypeId = CenterTypeId.InDependent,
                                Description = "غير مستقل"
                            },
                            new CenterTypeDescription
                            {
                                Id = 3,
                                FKLanguageId = LanguageId.English,
                                FKCenterTypeId = CenterTypeId.Dependent,
                                Description = "Dependent"
                            },
                            new CenterTypeDescription
                            {
                                Id = 4,
                                FKLanguageId = LanguageId.Arabic,
                                FKCenterTypeId = CenterTypeId.Dependent,
                                Description = "مستقل"
                            }
                        });
            #endregion

            #region CenterStatusDescription
            modelBuilder.Entity<CenterStatusDescription>()
                        .Property(csd => csd.FKCenterStatusId)
                        .HasConversion<int>();

            modelBuilder.Entity<CenterStatusDescription>()
                        .Property(csd => csd.FKLanguageId)
                        .HasConversion<int>();

            modelBuilder.Entity<CenterStatusDescription>()
                        .HasIndex(l => new
                        {
                            l.FKLanguageId,
                            l.FKCenterStatusId
                        })
                        .IsUnique();

            modelBuilder.Entity<CenterStatusDescription>()
                        .HasData(new List<CenterStatusDescription>
                        {
                            new CenterStatusDescription
                            {
                                Id = 1,
                                FKLanguageId = LanguageId.English,
                                FKCenterStatusId = CenterStatusId.UnderConsideration,
                                Description = "Under Consideration"
                            },
                            new CenterStatusDescription
                            {
                                Id = 2,
                                FKLanguageId = LanguageId.Arabic,
                                FKCenterStatusId = CenterStatusId.UnderConsideration,
                                Description = "قيد الدراسة"
                            },
                            new CenterStatusDescription
                            {
                                Id = 3,
                                FKLanguageId = LanguageId.English,
                                FKCenterStatusId = CenterStatusId.Approved,
                                Description = "Approved"
                            },
                            new CenterStatusDescription
                            {
                                Id = 4,
                                FKLanguageId = LanguageId.Arabic,
                                FKCenterStatusId = CenterStatusId.Approved,
                                Description = "مقبول"
                            },
                            new CenterStatusDescription
                            {
                                Id = 5,
                                FKLanguageId = LanguageId.English,
                                FKCenterStatusId = CenterStatusId.Rejected,
                                Description = "Rejected"
                            },
                            new CenterStatusDescription
                            {
                                Id = 6,
                                FKLanguageId = LanguageId.Arabic,
                                FKCenterStatusId = CenterStatusId.Rejected,
                                Description = "مرفوض"
                            }
                        });
            #endregion

            #region LocalizedProperty
            modelBuilder.Entity<LocalizedProperty>()
                        .Property(e => e.FKLanguageId)
                        .HasConversion<int>();

            modelBuilder.Entity<LocalizedProperty>()
                        .HasIndex(l => l.LocaleKeyGroup);

            modelBuilder.Entity<LocalizedProperty>()
                        .HasIndex(l => new
                        {
                            l.LocaleKeyGroup,
                            l.LocaleKey,
                            l.FKLanguageId,
                            l.EntityId
                        })
                        .IsUnique();
            #endregion

            #region ApplicationUser
            modelBuilder.Entity<ApplicationUser>()
                        .Property(e => e.FKUserTypeId)
                        .HasConversion<int>();

            modelBuilder.Entity<ApplicationUser>()
                       .Property(e => e.FKGenderId)
                       .HasConversion<int>();
            #endregion

            #region Center
            modelBuilder.Entity<Center>()
                        .HasIndex(u => u.Name)
                        .IsUnique();

            modelBuilder.Entity<Center>()
                        .HasIndex(u => u.IBAN)
                        .IsUnique();

            modelBuilder.Entity<Center>()
                       .Property(e => e.FKCenterTypeId)
                       .HasConversion<int>();

            modelBuilder.Entity<Center>()
                       .Property(e => e.FKCenterStatusId)
                       .HasConversion<int>();
            #endregion

            #region Roles/Users
            //Add roles except Admin/Center role
            modelBuilder.Entity<IdentityRole>().HasData(Enum.GetValues(typeof(Role))
                                                            .Cast<Role>()
                                                            .Where(r => r != Role.Administrator && r != Role.CenterApprover)
                                                            .Select(e => new IdentityRole
                                                            {
                                                                Name = e.ToString(),
                                                                NormalizedName = e.ToString()
                                                            }));

            //Add Admin role manually
            var adminRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = Role.Administrator.ToString(),
                NormalizedName = Role.Administrator.ToString()
            });

            //Add Center Reviewer role manually
            var centerReviewerRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = centerReviewerRoleId,
                Name = Role.CenterApprover.ToString(),
                NormalizedName = Role.CenterApprover.ToString()
            });

            //Add users
            var hasher = new PasswordHasher<IdentityUser>();
            var adminId = Guid.NewGuid().ToString();
            var centerReviewerId = Guid.NewGuid().ToString();
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminId,
                    FirstName = "Administrator",
                    FourthName = "",
                    IdNumber = "",
                    MobileNumber = "",
                    FKGenderId = GenderId.Male,
                    UserName = "Admin",
                    NormalizedUserName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                    Email = "Admin@Qurrah.com",
                    NormalizedEmail = "Admin@Qurrah.com",
                    PhoneNumber = "0543700744",
                    CreatedOn = DateTime.Now,
                    FKUserTypeId = UserTypeId.Administrator
                },
                new ApplicationUser
                {
                    Id = centerReviewerId,
                    FirstName = "Reviewer",
                    FourthName = "",
                    IdNumber = "",
                    MobileNumber = "",
                    FKGenderId = GenderId.Male,
                    UserName = "CenterReviewer",
                    NormalizedUserName = "CenterReviewer",
                    PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                    Email = "CenterReviewer@Qurrah.com",
                    NormalizedEmail = "CenterReviewer@Qurrah.com",
                    PhoneNumber = "0543700745",
                    CreatedOn = DateTime.Now,
                    FKUserTypeId = UserTypeId.CenterApprover
                });

            //Assign Admin role to user
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = centerReviewerRoleId,
                    UserId = centerReviewerId
                });
            #endregion

            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
        #endregion
    }
}