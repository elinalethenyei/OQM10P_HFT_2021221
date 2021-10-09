using Microsoft.EntityFrameworkCore;
using OQM10P_HFT_2021221.Models;

namespace OQM10P_HFT_2021221.Repository
{
    public class IssueManagementDbContext : DbContext
    {
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        public IssueManagementDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|IssueManagementDb.mdf;Trusted_Connection=Yes;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
