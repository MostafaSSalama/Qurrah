using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Qurrah.Entities;

namespace Qurrah.Data
{
    public class QurrahDbContext : IdentityDbContext
    {
        public QurrahDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<FAQType> FAQType { get; set; }
    }
}