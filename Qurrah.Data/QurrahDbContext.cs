using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Qurrah.Entities;
using Qurrah.Entities.NotMapped;

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
        public DbSet<Gender> Gender { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<FAQType> FAQType { get; set; }
        #endregion

        #region d
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
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}