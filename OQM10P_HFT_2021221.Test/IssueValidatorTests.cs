using Moq;
using NUnit.Framework;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Validators;
using System.Collections.Generic;
using System.Linq;

namespace OQM10P_HFT_2021221.Test
{
    [TestFixture]
    public class IssueValidatorTests
    {
        #region Constants
        const int id1 = 1;
        const int id2 = 2;
        #endregion

        [Test]
        public void ValidateIssueValidTest()
        {
            //Arrange
            var issueRepo = new Mock<IIssueRepo>();
            issueRepo.Setup(x => x.ReadAll()).Returns(Enumerable.Empty<Issue>().AsQueryable());

            var userRepo = new Mock<IUserRepo>();

            Project project1 = new Project() { Id = id1, Name = "Teszt Project 1", GoalDescription = "Valami cél", OwnerId = id1 };
            List<Project> projects = new List<Project>();
            projects.Add(project1);

            var projectRepo = new Mock<IProjectRepo>();
            projectRepo.Setup(x => x.ReadAll()).Returns(projects.AsQueryable());
            projectRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(projects[0]);
            var validator = new IssueValidator(issueRepo.Object, userRepo.Object, projectRepo.Object);

            //Act
            var result = validator.Validate(new Issue() { Title = "Teszt issue", ProjectId = project1.Id });

            //Assert
            Assert.IsEmpty(result);
        }


        [TestCaseSource(nameof(GetInvalidIssueTestData))]
        public void ValidateIssueInvalidIssueTests(List<Issue> issues, List<Project> projects, List<User> users, Issue issueToValidate, string expectedErrorMessage)
        {
            //Arrange
            var userRepo = new Mock<IUserRepo>();
            userRepo.Setup(x => x.ReadAll()).Returns(users.AsQueryable());
            userRepo.Setup(x => x.Read(id1)).Returns(users[0]);
            userRepo.Setup(x => x.Read(id2)).Returns((User)null);

            var projectRepo = new Mock<IProjectRepo>();
            projectRepo.Setup(x => x.ReadAll()).Returns(projects.AsQueryable());
            projectRepo.Setup(x => x.Read(id1)).Returns(projects[0]);
            projectRepo.Setup(x => x.Read(id2)).Returns((Project)null);

            var issueRepo = new Mock<IIssueRepo>();
            issueRepo.Setup(x => x.ReadAll()).Returns(issues.AsQueryable());
            issueRepo.Setup(x => x.Read(id1)).Returns(issues[0]);
            issueRepo.Setup(x => x.Read(id2)).Returns((Issue)null);


            var validator = new IssueValidator(issueRepo.Object, userRepo.Object, projectRepo.Object);


            //Act
            var result = validator.Validate(issueToValidate);

            //Assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(expectedErrorMessage, result[0].ErrorMessage);

        }

        #region Utils

        static List<TestCaseData> GetInvalidIssueTestData()
        {
            var testData = new List<TestCaseData>();

            User user1 = new User() { Id = id1, Name = "Teszt Elek", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
            User user2 = new User() { Id = id2, Name = "Másik Teszt Elek", Email = "user2@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user2" };
            List<User> users = new List<User>();
            users.Add(user1);

            Project project1 = new Project() { Id = id1, Name = "Teszt Project 1", GoalDescription = "Valami cél", OwnerId = user1.Id, Owner = user1 };
            Project project2 = new Project() { Id = id2, Name = "Teszt Project 2", GoalDescription = "Valami másik cél", OwnerId = user2.Id, Owner = user2 };
            List<Project> projects = new List<Project>();
            projects.Add(project1);

            var issue1 = new Issue() { Id = id1, Title = "Teszt feladat 1", ProjectId = id1, Project=project1 };
            var issue2 = new Issue() { Id=id2, Title = "Teszt feladat 2", ProjectId = id2, Project = project2 };
            List<Issue> issues = new List<Issue>();
            issues.Add(issue1);

            //Nem mentett id-t akarunk updatelni
            testData.Add(new TestCaseData(issues, projects, users, new Issue() { Id=issue2.Id, Title = issue1.Title, ProjectId = issue1.ProjectId, Project = issue1.Project }, $"Issue with the given id does not exists! Id: {issue2.Id}"));
            testData.Add(new TestCaseData(issues, projects, users, new Issue() { Title = issue1.Title, ProjectId = issue1.ProjectId, Project = issue1.Project, Status=IssueStatus.INPROGRESS }, "Issue without user has to be in TODO status!"));
            testData.Add(new TestCaseData(issues, projects, users, new Issue() {Title = issue1.Title, ProjectId = issue1.ProjectId, Project = issue1.Project, UserId=user2.Id, User=user2 }, $"Issue\'s user does not exist with the given id! User id: {user2.Id}"));
            testData.Add(new TestCaseData(issues, projects, users, new Issue() { Title = issue1.Title, ProjectId = issue2.ProjectId, Project = issue2.Project }, $"Issue\'s project does not exist with the given id! Project id: {issue2.ProjectId}"));
            testData.Add(new TestCaseData(issues, projects, users, new Issue() {Title = null, ProjectId = issue1.ProjectId, Project = issue1.Project }, "The Title field is required."));

            return testData;
        }

        #endregion


    }
}
