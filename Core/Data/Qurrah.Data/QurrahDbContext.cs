using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Qurrah.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using Qurrah.Entities.NoMapping;

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
        public DbSet<UserType> UserType { get; set; }
        public DbSet<ParentUser> ParentUser { get; set; }
        public DbSet<CenterOwnerUser> CenterOwnerUser { get; set; }
        public DbSet<Center> Center { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<FAQType> FAQType { get; set; }
        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FAQ
            modelBuilder.Entity<FAQ>()
                        .HasOne(u => u.FAQType)
                        .WithMany(t => t.FAQs)
                        .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region ApplicationUser
            modelBuilder.Entity<ApplicationUser>()
                        .Property(e => e.FKUserTypeId)
                        .HasConversion<byte>();

            modelBuilder.Entity<UserType>().HasData(Enum.GetValues(typeof(UserTypeId))
                                                        .Cast<UserTypeId>()
                                                        .Select(e => new UserType()
                                                        {
                                                            Id = e,
                                                            Name = e.ToString()
                                                        }));

            modelBuilder.Entity<ApplicationUser>()
                        .HasOne(u => u.UserType)
                        .WithMany(t => t.ApplicationUsers)
                        .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region ParentUser
            modelBuilder.Entity<ParentUser>()
                        .Property(e => e.FKGenderId)
                        .HasConversion<byte>();

            modelBuilder.Entity<Gender>().HasData(Enum.GetValues(typeof(GenderId))
                                                      .Cast<GenderId>()
                                                      .Select(e => new Gender()
                                                      {
                                                          Id = e,
                                                          Name = e.ToString()
                                                      }));

            modelBuilder.Entity<ParentUser>()
                        .HasOne(u => u.Gender)
                        .WithMany(t => t.ParentUsers)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ParentUser>()
                        .HasIndex(u => u.FKUserId)
                        .IsUnique();

            modelBuilder.Entity<ParentUser>()
                        .HasOne("ApplicationUser")
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region CenterOwnerUser
            modelBuilder.Entity<CenterOwnerUser>()
                        .HasIndex(u => u.FKUserId)
                        .IsUnique();
            modelBuilder.Entity<Center>()
                        .HasIndex(u => u.Name)
                        .IsUnique();

            modelBuilder.Entity<CenterOwnerUser>()
                        .HasOne("Center")
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CenterOwnerUser>()
                        .HasOne("ApplicationUser")
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Roles/Users
            //Add roles except Admin/Center role
            modelBuilder.Entity<IdentityRole>().HasData(Enum.GetValues(typeof(Role))
                                                            .Cast<Role>()
                                                            .Where(r => r != Role.Administrator && r!=Role.CenterApprover)
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
        }
        #endregion
    }
}