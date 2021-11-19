using Moq;
using NUnit.Framework;
using OQM10P_HFT_2021221.Logic.Services;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Models.ResponseObjects;
using OQM10P_HFT_2021221.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Test
{
    [TestFixture]
    public class ReportServiceTests
    {
        #region Constants
        private static DateTime baseDueDate = new DateTime(2021, 10, 10);
        private static DateTime closedAtBeforeDueDate = new DateTime(2021, 10, 1);
        private static DateTime closedAtAfterDueDate = new DateTime(2021, 11, 1);
        private static User defaultProjectOwner = new User() { Id = 1, Name = "Male 1", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
        private static Project defaultProject = new Project() { Id = 1, Name = "Teszt Project 1", OwnerId = defaultProjectOwner.Id, Owner = defaultProjectOwner };

        private const int defaultEstimatedTime = 10;
        private const int defaultTimeSpentLessThanEstimated = 8;
        private const int defaultTimeSpentMoreThanEstimated = 12;
        private const int defaultTimeSpentEqualToEstimated = defaultEstimatedTime;

        #endregion

        [Test]
        public void GetDoneIssueCountByUserSexInDueDateTest()
        {
            //Arrange
            var issueRepo = new Mock<IIssueRepo>();
            var userRepo = new Mock<IUserRepo>();
            var projectRepo = new Mock<IProjectRepo>();

            User maleUser1 = new User() { Id = 1, Name = "Male 1", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
            User maleUser2 = new User() { Id = 2, Name = "Male 2", Email = "user2@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user2" };
            User femaleUser1 = new User() { Id = 3, Name = "Female 1", Email = "user3@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.FEMALE, Username = "user3" };
            User femaleUser2 = new User() { Id = 4, Name = "Female 2", Email = "user4@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.FEMALE, Username = "user4" };

            List<Issue> issues = new List<Issue>()
            {
                //Male userek által időben befejezett issuek: 3 db
                CreateTestIssue(1, baseDueDate, closedAtBeforeDueDate, maleUser1),
                CreateTestIssue(2, baseDueDate, closedAtBeforeDueDate, maleUser1),
                CreateTestIssue(3, baseDueDate, closedAtBeforeDueDate, maleUser2),
                //Female userek által időben befejezett issuek: 4 db
                CreateTestIssue(4, baseDueDate, closedAtBeforeDueDate, femaleUser1),
                CreateTestIssue(5, baseDueDate, closedAtBeforeDueDate, femaleUser1),
                CreateTestIssue(6, baseDueDate, closedAtBeforeDueDate, femaleUser1),
                CreateTestIssue(7, baseDueDate, closedAtBeforeDueDate, femaleUser2),
                //Male userek által határidő után befejezett issuek: 2 db -> nem jelenik meg a statisztikában
                CreateTestIssue(8, baseDueDate, closedAtAfterDueDate, maleUser1),
                CreateTestIssue(9, baseDueDate, closedAtAfterDueDate, maleUser2),
                //Female userek által határidő után befejezett issuek: 2 db -> nem jelenik meg a statisztikában
                CreateTestIssue(10, baseDueDate, closedAtAfterDueDate, femaleUser1),
                CreateTestIssue(11, baseDueDate, closedAtAfterDueDate, femaleUser1),
                //DueDate nélküli issuek: 2 db -> nem jelenik meg a statisztikában
                CreateTestIssue(12, null, closedAtAfterDueDate, maleUser1),
                CreateTestIssue(13, null, closedAtAfterDueDate, femaleUser1),
                //Nem DONE státuszú issuek: 2 db -> nem jelenik meg a statisztikában
                CreateTestIssue(14, baseDueDate, null, femaleUser1, IssueStatus.INPROGRESS ),
                CreateTestIssue(15, baseDueDate, null, femaleUser2, IssueStatus.TODO),
                //User nélküli issuek: 1 db -> nem jelenik meg a statisztikában
                CreateTestIssue(16, baseDueDate, null, null, IssueStatus.TODO)
            };

            issueRepo.Setup(x => x.ReadAll()).Returns(issues.AsQueryable());
            var reportService = new ReportService(userRepo.Object, projectRepo.Object, issueRepo.Object);

            //Act

            Dictionary<UserSexType, int> result = reportService.GetDoneIssueCountByUserSexInDueDate();

            //Assert
            Assert.IsNotEmpty(result);
            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.ContainsKey(UserSexType.FEMALE));
            Assert.IsTrue(result.ContainsKey(UserSexType.MALE));
            Assert.AreEqual(4, result.GetValueOrDefault(UserSexType.FEMALE));
            Assert.AreEqual(3, result.GetValueOrDefault(UserSexType.MALE));
        }

        [Test]
        public void GetTop3UserByClosedIssuesTest()
        {
            //Arrange
            var issueRepo = new Mock<IIssueRepo>();
            var userRepo = new Mock<IUserRepo>();
            var projectRepo = new Mock<IProjectRepo>();

            User user1 = new User() { Id = 1, Name = "Felhasználó 1", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
            User user2 = new User() { Id = 2, Name = "Felhasználó 2", Email = "user2@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user2" };
            User user3 = new User() { Id = 3, Name = "Felhasználó 3", Email = "user3@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.FEMALE, Username = "user3" };
            User user4 = new User() { Id = 4, Name = "Felhasználó 4", Email = "user4@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.FEMALE, Username = "user4" };

            List<Issue> issues = new List<Issue>()
            {
                //User1 által lezárt issuek: 3 db
                CreateTestIssue(1, null, closedAtBeforeDueDate, user1),
                CreateTestIssue(2, null, closedAtBeforeDueDate, user1),
                CreateTestIssue(3, null, closedAtBeforeDueDate, user1),
                //User1 által nem lezárt issuek: 1 db -> nem kerül be a riportba
                CreateTestIssue(4, null, closedAtBeforeDueDate, user1, IssueStatus.TODO),
                //User2 által lezárt issuek: 3 db
                CreateTestIssue(5, null, closedAtBeforeDueDate, user2),
                CreateTestIssue(6, null, closedAtBeforeDueDate, user2),
                //User2 által nem lezárt issuek: 1 db -> nem kerül be a riportba
                CreateTestIssue(7, null, null, user2, IssueStatus.INPROGRESS),
                //User3 által lezárt issuek: 3 db
                CreateTestIssue(8, null, closedAtBeforeDueDate, user3),
                CreateTestIssue(8, null, closedAtBeforeDueDate, user3),
                //User3 által nem lezárt issuek: 1 db -> nem kerül be a riportba
                CreateTestIssue(9, null, null, user3, IssueStatus.TODO),
                //User4 által lezárt issuek: 1 db -> nem kerül be a riportba
                CreateTestIssue(10, null, closedAtBeforeDueDate, user4),
                //User4 által nem lezárt issuek: 1 db -> nem kerül be a riportba
                CreateTestIssue(11, null, null, user4, IssueStatus.INPROGRESS),
                //Userhez még nem rendelt issuek: 1 db -> nem kerül be a riportba 
                CreateTestIssue(11, null, null, null, IssueStatus.TODO)
            };

            issueRepo.Setup(x => x.ReadAll()).Returns(issues.AsQueryable());
            userRepo.Setup(x => x.ReadAll()).Returns((new List<User>() { user1, user2, user3, user4 }).AsQueryable());
            var reportService = new ReportService(userRepo.Object, projectRepo.Object, issueRepo.Object);

            //Act

            Dictionary<string, int> result = reportService.GetTop3UserByClosedIssues();

            //Assert
            Assert.IsNotEmpty(result);
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.ContainsKey(user1.Name));
            Assert.IsTrue(result.ContainsKey(user2.Name));
            Assert.IsTrue(result.ContainsKey(user3.Name));
            Assert.AreEqual(3, result.GetValueOrDefault(user1.Name));
            Assert.AreEqual(2, result.GetValueOrDefault(user2.Name));
            Assert.AreEqual(2, result.GetValueOrDefault(user3.Name));

        }

        [Test]
        public void GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProjectTest()
        {
            //Arrange
            var issueRepo = new Mock<IIssueRepo>();
            var userRepo = new Mock<IUserRepo>();
            var projectRepo = new Mock<IProjectRepo>();

            User projectOwnerUser1 = new User() { Id = 1, Name = "Project owner 1", Email = "user1@teszt.com", Position = UserPositionType.SENIOR_DEV, Sex = UserSexType.FEMALE, Username = "user1" };
            User projectOwnerUser2 = new User() { Id = 2, Name = "Project owner 2", Email = "user2@teszt.com", Position = UserPositionType.SENIOR_DEV, Sex = UserSexType.FEMALE, Username = "user2" };

            Project project1 = new Project() { Id = 1, Name = "Teszt Project 1", OwnerId = projectOwnerUser1.Id, Owner = projectOwnerUser1 };
            Project project2 = new Project() { Id = 2, Name = "Teszt Project 2", OwnerId = projectOwnerUser2.Id, Owner = projectOwnerUser2 };

            User user1 = new User() { Id = 3, Name = "Felhasználó 1", Email = "user3@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.FEMALE, Username = "user3" };

            List<Issue> issues = new List<Issue>()
            {
                //project1 top priority done issuek időn belül: 3 db
                CreateTestIssueWithPriorityAndTimes(1, user1, project1, defaultTimeSpentLessThanEstimated),
                CreateTestIssueWithPriorityAndTimes(2, user1, project1, defaultTimeSpentLessThanEstimated),
                CreateTestIssueWithPriorityAndTimes(3, user1, project1, defaultTimeSpentLessThanEstimated),
                //project1 NEM top priority done issuek időn belül: 1 db -> nem kerül be a riportba
                CreateTestIssueWithPriorityAndTimes(4, user1, project1, defaultTimeSpentLessThanEstimated, priority:IssuePriorityType.SMALL),
                //project1 top priority NEM done issuek: 1 db -> nem kerül be a riportba
                CreateTestIssueWithPriorityAndTimes(5, user1, project1, null, status:IssueStatus.INPROGRESS),
                //project1 top priority done issuek időn túl: 1 db -> nem kerül be a riportba
                CreateTestIssueWithPriorityAndTimes(6, user1, project1, defaultTimeSpentMoreThanEstimated),
                //project1 top priority done issuek, de nincs sem estimated sem pedig spent time-ja: 1 db -> nem kerül be a riportba
                 new Issue() { Id = 7, Title = "Teszt feladat", ProjectId = project1.Id, Project = project1, Status = IssueStatus.DONE, Priority = IssuePriorityType.HIGH, User=user1, UserId = user1.Id, ClosedAt=closedAtBeforeDueDate},
                //project2 top priority done issuek időn belül: 2 db
                CreateTestIssueWithPriorityAndTimes(7, user1, project2, defaultTimeSpentLessThanEstimated),
                CreateTestIssueWithPriorityAndTimes(8, user1, project2, defaultTimeSpentLessThanEstimated)
            };

            issueRepo.Setup(x => x.ReadAll()).Returns(issues.AsQueryable);
            userRepo.Setup(x => x.ReadAll()).Returns(new List<User>() { projectOwnerUser1, projectOwnerUser2, user1 }.AsQueryable);
            projectRepo.Setup(x => x.ReadAll()).Returns(new List<Project>() { project1, project2 }.AsQueryable());
            var reportService = new ReportService(userRepo.Object, projectRepo.Object, issueRepo.Object);

            //Act

            TopPriorityIssueSolverProjectOwnerResponse result = reportService.GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject();

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(project1.Name, result.ProjectName);
            Assert.AreEqual(project1.Owner.Name, result.OwnerName);
            Assert.AreEqual(3, result.IssueCount);
        }

        [Test]
        public void GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProjectWithNoResultTest()
        {
            //Arrange
            var issueRepo = new Mock<IIssueRepo>();
            var userRepo = new Mock<IUserRepo>();
            var projectRepo = new Mock<IProjectRepo>();

            User projectOwnerUser1 = new User() { Id = 1, Name = "Project owner 1", Email = "user1@teszt.com", Position = UserPositionType.SENIOR_DEV, Sex = UserSexType.FEMALE, Username = "user1" };
            User projectOwnerUser2 = new User() { Id = 2, Name = "Project owner 2", Email = "user2@teszt.com", Position = UserPositionType.SENIOR_DEV, Sex = UserSexType.FEMALE, Username = "user2" };

            Project project1 = new Project() { Id = 1, Name = "Teszt Project 1", OwnerId = projectOwnerUser1.Id, Owner = projectOwnerUser1 };
            Project project2 = new Project() { Id = 2, Name = "Teszt Project 2", OwnerId = projectOwnerUser2.Id, Owner = projectOwnerUser2 };

            User user1 = new User() { Id = 3, Name = "Felhasználó 1", Email = "user3@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.FEMALE, Username = "user3" };

            List<Issue> issues = new List<Issue>()
            {
                //project1 BUG issuek: 4 db
                new Issue() { Id = 1, Title = "Teszt feladat", ProjectId = project1.Id, Project = project1, Status = IssueStatus.DONE, Priority = IssuePriorityType.HIGH, User=user1, UserId = user1.Id, ClosedAt=closedAtBeforeDueDate},
                new Issue() { Id = 2, Title = "Teszt feladat", ProjectId = project1.Id, Project = project1, Status = IssueStatus.DONE, Priority = IssuePriorityType.HIGH, User=user1, UserId = user1.Id, ClosedAt=closedAtBeforeDueDate},
                CreateTestIssueWithPriorityAndTimes(3, user1, project1, defaultTimeSpentMoreThanEstimated),
                //project2 -> nincs számolható issue-ja
                new Issue() { Id = 4, Title = "Teszt feladat", ProjectId = project2.Id, Project = project2, Status = IssueStatus.DONE, Priority = IssuePriorityType.HIGH, User=user1, UserId = user1.Id, ClosedAt=closedAtBeforeDueDate},
                CreateTestIssueWithPriorityAndTimes(5, user1, project2, null, status:IssueStatus.INPROGRESS)
            };

            issueRepo.Setup(x => x.ReadAll()).Returns(issues.AsQueryable);
            userRepo.Setup(x => x.ReadAll()).Returns(new List<User>() { projectOwnerUser1, projectOwnerUser2, user1 }.AsQueryable);
            projectRepo.Setup(x => x.ReadAll()).Returns(new List<Project>() { project1, project2 }.AsQueryable());
            var reportService = new ReportService(userRepo.Object, projectRepo.Object, issueRepo.Object);

            //Act

            TopPriorityIssueSolverProjectOwnerResponse result = reportService.GetOwnerOfFirstTopPriorityIssuesBeenSolvedInProject();

            //Assert
            Assert.Null(result);
        }

        [Test]
        public void GetTop3ProjectWithFewBugsTest()
        {
            //Arrange
            var issueRepo = new Mock<IIssueRepo>();
            var userRepo = new Mock<IUserRepo>();
            var projectRepo = new Mock<IProjectRepo>();

            User projectOwnerUser1 = new User() { Id = 1, Name = "Project owner 1", Email = "user1@teszt.com", Position = UserPositionType.SENIOR_DEV, Sex = UserSexType.FEMALE, Username = "user1" };

            Project project1 = new Project() { Id = 1, Name = "Teszt Project 1", OwnerId = projectOwnerUser1.Id, Owner = projectOwnerUser1 };
            Project project2 = new Project() { Id = 2, Name = "Teszt Project 2", OwnerId = projectOwnerUser1.Id, Owner = projectOwnerUser1 };
            Project project3 = new Project() { Id = 3, Name = "Teszt Project 3", OwnerId = projectOwnerUser1.Id, Owner = projectOwnerUser1 };
            Project project4 = new Project() { Id = 4, Name = "Teszt Project 4", OwnerId = projectOwnerUser1.Id, Owner = projectOwnerUser1 };

            List<Issue> issues = new List<Issue>()
            {
                //project1 bugs
                CreateTestIssueWithIssueType(1, project1),
                CreateTestIssueWithIssueType(2, project1),
                CreateTestIssueWithIssueType(3, project1),
                CreateTestIssueWithIssueType(4, project1),
                //project1 task
                CreateTestIssueWithIssueType(5, project1, IssueType.TASK),
                CreateTestIssueWithIssueType(6, project1, IssueType.TASK),
                //project2 bugs
                CreateTestIssueWithIssueType(1, project2),
                CreateTestIssueWithIssueType(2, project2),
                //project2 task
                CreateTestIssueWithIssueType(5, project2, IssueType.TASK),
                //project3 bugs
                CreateTestIssueWithIssueType(1, project3),
                CreateTestIssueWithIssueType(2, project3),
                //project1 task
                CreateTestIssueWithIssueType(5, project3, IssueType.TASK),
                //project4 bugs
                CreateTestIssueWithIssueType(1, project4),
                //project1 task
                CreateTestIssueWithIssueType(5, project4, IssueType.TASK),
            };

            issueRepo.Setup(x => x.ReadAll()).Returns(issues.AsQueryable);
            projectRepo.Setup(x => x.ReadAll()).Returns(new List<Project>() { project1, project2, project3, project4 }.AsQueryable());
            var reportService = new ReportService(userRepo.Object, projectRepo.Object, issueRepo.Object);

            //Act

            Dictionary<string, int> result = reportService.GetTop3ProjectWithFewBugs();

            //Assert
            Assert.IsNotEmpty(result);
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.ContainsKey(project4.Name));
            Assert.IsTrue(result.ContainsKey(project3.Name));
            Assert.IsTrue(result.ContainsKey(project2.Name));
            Assert.AreEqual(1, result.GetValueOrDefault(project4.Name));
            Assert.AreEqual(2, result.GetValueOrDefault(project3.Name));
            Assert.AreEqual(2, result.GetValueOrDefault(project2.Name));
        }



        #region Utils

        static Issue CreateTestIssue(int id, DateTime? dueDate, DateTime? closedAt, User? user, IssueStatus status = IssueStatus.DONE)
        {
            var issue = new Issue() { Id = id, Title = "Teszt feladat", ProjectId = defaultProject.Id, Project = defaultProject, DueDate = dueDate, ClosedAt = closedAt, Status = status };
            if (user != null)
            {
                issue.User = user;
                issue.UserId = user.Id;
            }
            return issue;
        }

        static Issue CreateTestIssueWithPriorityAndTimes(int id, User? user, Project project, int? timeSpent, int estimatedTime = defaultEstimatedTime, IssueStatus status = IssueStatus.DONE, IssuePriorityType priority = IssuePriorityType.HIGH)
        {
            var issue = new Issue() { Id = id, Title = "Teszt feladat", ProjectId = project.Id, Project = project, Status = status, Priority = priority, EstimatedTime = estimatedTime };
            if (user != null)
            {
                issue.User = user;
                issue.UserId = user.Id;
            }
            if (timeSpent != null)
            {
                issue.TimeSpent = (int)timeSpent;
            }
            if (status.Equals(IssueStatus.DONE))
            {
                issue.ClosedAt = closedAtBeforeDueDate;
            }
            return issue;
        }

        static Issue CreateTestIssueWithIssueType(int id, Project project, IssueType type = IssueType.BUG)
        {
            var issue = new Issue() { Id = id, Title = "Teszt feladat", ProjectId = project.Id, Project = project, Type = type };
            return issue;
        }
        #endregion
    }
}
