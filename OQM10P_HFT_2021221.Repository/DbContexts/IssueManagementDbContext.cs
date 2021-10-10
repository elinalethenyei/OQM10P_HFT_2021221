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
                .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\IssueManagementDb.mdf;Integrated Security=true;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>(e => e.HasOne(p => p.Project).WithMany(i => i.Issues).HasForeignKey(p => p.ProjectId).OnDelete(DeleteBehavior.ClientSetNull));

            //seed

            var user1 = new User() { Id=1, Name = "asd", Email = "a", Position = UserPositionType.MEDIOR_DEV, Sex = UserSexType.FEMALE, Username = "a" };
            var proj1 = new Project("proj1", user1.Id);
            var issue1 = new Issue() { Title = "asd", Description = "asd", ProjectId=proj1.Id};

            modelBuilder.Entity<User>().HasData(user1);
            modelBuilder.Entity<Project>().HasData(proj1);
            modelBuilder.Entity<Issue>().HasData(issue1);
        }
    }
}
