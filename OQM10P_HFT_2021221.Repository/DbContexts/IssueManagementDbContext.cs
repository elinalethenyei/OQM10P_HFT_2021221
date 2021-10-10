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

            //Database.ExecuteSqlRaw("insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (1, 'Inez Melsom', 'imelsom0@prnewswire.com', 'MALE', 'imelsom0', 'JUNIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (2, 'Samuele Ledner', 'sledner1@statcounter.com', 'FEMALE', 'sledner1', 'JUNIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (3, 'Eleonora Figgen', 'efiggen2@netscape.com', 'MALE', 'efiggen2', 'JUNIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (4, 'Gustaf Franceschelli', 'gfranceschelli3@xinhuanet.com', 'MALE', 'gfranceschelli3', 'JUNIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (5, 'Skip Varvell', 'svarvell4@chron.com', 'MALE', 'svarvell4', 'MEDIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (6, 'Mina Irlam', 'mirlam5@timesonline.co.uk', 'FEMALE', 'mirlam5', 'JUNIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (7, 'Duffy Shearmer', 'dshearmer6@youtu.be', 'MALE', 'dshearmer6', 'SENIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (8, 'Micheal Stivens', 'mstivens7@cbslocal.com', 'MALE', 'mstivens7', 'SENIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (9, 'Kimberli Viger', 'kviger8@woothemes.com', 'MALE', 'kviger8', 'MEDIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (10, 'Almeta Nice', 'anice9@wikipedia.org', 'MALE', 'anice9', 'SENIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (11, 'Jayme Rojas', 'jrojasa@sohu.com', 'MALE', 'jrojasa', 'MEDIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (12, 'Sharl Erlam', 'serlamb@yelp.com', 'FEMALE', 'serlamb', 'MEDIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (13, 'Waylin Armitage', 'warmitagec@fc2.com', 'MALE', 'warmitagec', 'MEDIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (14, 'Cal Cleaves', 'ccleavesd@cyberchimps.com', 'FEMALE', 'ccleavesd', 'SENIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (15, 'Kimmie Manicomb', 'kmanicombe@businesswire.com', 'FEMALE', 'kmanicombe', 'JUNIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (16, 'Fanchette Drinnan', 'fdrinnanf@nasa.gov', 'FEMALE', 'fdrinnanf', 'SENIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (17, 'Maisie Hilland', 'mhillandg@nbcnews.com', 'FEMALE', 'mhillandg', 'JUNIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (18, 'Keenan Andrys', 'kandrysh@sakura.ne.jp', 'MALE', 'kandrysh', 'JUNIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (19, 'Devonne Height', 'dheighti@cocolog-nifty.com', 'FEMALE', 'dheighti', 'MEDIOR_DEV'); insert into USER (ID, NAME, EMAIL, SEX, USERNAME, POSITION) values (20, 'Abbey Cowburn', 'acowburnj@amazon.de', 'MALE', 'acowburnj', 'MEDIOR_DEV');");

            //SaveChanges();

            var user1 = new User() { Id = 1, Name = "asd", Email = "a", Position = UserPositionType.MEDIOR_DEV, Sex = UserSexType.FEMALE, Username = "a" };
            var proj1 = new Project("proj1", 1) { Id = 1 , OwnerId = 1};
            var issue1 = new Issue() {Id= 1, Title = "asd", Description = "asd", ProjectId=proj1.Id};

            modelBuilder.Entity<User>().HasData(user1);
            modelBuilder.Entity<Project>().HasData(proj1);
            modelBuilder.Entity<Issue>().HasData(issue1);

            



        }
    }
}
