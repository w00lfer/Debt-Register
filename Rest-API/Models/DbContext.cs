using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Rest_API.Models
{
    public class DbContext : IdentityDbContext
    {
        public DbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Debt> Debts { get; set; }
    }
}
