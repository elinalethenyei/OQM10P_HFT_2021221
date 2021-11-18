using Moq;
using NUnit.Framework;
using OQM10P_HFT_2021221.Logic.Services;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Test
{
    [TestFixture]
    public class UserServiceTests
    {
        #region Constants
        private static DateTime baseDueDate = new DateTime(2021, 10, 10);
        private static DateTime closedAtBeforeDueDate = new DateTime(2021, 10, 1);
        private static DateTime closedAtAfterDueDate = new DateTime(2021, 11, 1);
        private static User projectOwnerUser = new User() { Id = 1, Name = "Male 1", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
        private static Project project = new Project() { Id = 1, Name = "Teszt Project 1", OwnerId = projectOwnerUser.Id, Owner = projectOwnerUser };

        #endregion

        [Test]
        public void GetDoneIssueCountByUserSexInDueDateTest()
        {
            //Arrange
            var issueRepo = new Mock<IIssueRepo>();
            var userRepo = new Mock<IUserRepo>();
            var projectRepo = new Mock<IProjectRepo>();
            var validator = new Mock<IModelValidator>();

            User maleUser1 = new User() { Id = 1, Name = "Male 1", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
            User maleUser2 = new User() { Id = 2, Name = "Male 2", Email = "user2@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user2" };
            User femaleUser1 = new User() { Id = 3, Name = "Female 1", Email = "user3@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.FEMALE, Username = "user3" };
            User femaleUser2 = new User() { Id = 4, Name = "Female 2", Email = "user4@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.FEMALE, Username = "user4" };
            //List<User> users = new List<User>() { user1, user2, user3, user4};

            //Project project1 = new Project() { Id = 1, Name = "Teszt Project 1", OwnerId = maleUser1.Id, Owner = maleUser1 };
            //List<Project> projects = new List<Project>();
            //projects.Add(project1);

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
                CreateTestIssue(16, baseDueDate, null, null, IssueStatus.TODO),
            };

            issueRepo.Setup(x => x.ReadAll()).Returns(issues.AsQueryable);
            var userService = new UserService(userRepo.Object, projectRepo.Object, issueRepo.Object, validator.Object);

            //Act

            Dictionary<UserSexType, int> result = userService.GetDoneIssueCountByUserSexInDueDate();

            //Assert
            Assert.IsNotEmpty(result);
            Assert.IsTrue(result.Count() == 2);
            Assert.AreEqual(4, result.GetValueOrDefault(UserSexType.FEMALE));
            Assert.AreEqual(3, result.GetValueOrDefault(UserSexType.MALE));

        }

        #region Utils

        static Issue CreateTestIssue(int id, DateTime? dueDate, DateTime? closedAt, User? user, IssueStatus status = IssueStatus.DONE)
        {
            var issue = new Issue() { Id = id, Title = "Teszt feladat", ProjectId = project.Id, Project = project, DueDate = dueDate, ClosedAt = closedAt, Status = status };
            if (user != null)
            {
                issue.User = user;
                issue.UserId = user.Id;
            }
            return issue;
        }
        #endregion
    }
}
