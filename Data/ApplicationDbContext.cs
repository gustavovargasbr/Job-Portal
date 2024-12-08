using JOB_PORTAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace JOB_PORTAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet for Company
        public DbSet<Company> Companies { get; set; }

        // DbSet for custom Login
        public DbSet<Login> Logins { get; set; }

        // DbSet for custom Register
        public DbSet<Register> Registers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensures Identity-related tables are created

            // Additional configurations if required
        }
    }
}
