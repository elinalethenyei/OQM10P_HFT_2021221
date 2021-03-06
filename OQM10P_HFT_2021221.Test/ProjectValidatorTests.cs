
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
    public class ProjectValidatorTests
    {
        #region Constants

        const int id1 = 1;
        const int id2 = 2;

        #endregion

        [Test]
        public void ValidateProjectValidTest()
        {
            //Arrange
            User user1 = new User() { Id = 1, Name = "Teszt Elek", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
            List<User> users = new List<User>();
            users.Add(user1);
            var userRepo = new Mock<IUserRepo>();
            userRepo.Setup(x => x.ReadAll()).Returns(users.AsQueryable());
            userRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(user1);

            var projectRepo = new Mock<IProjectRepo>();
            projectRepo.Setup(x => x.ReadAll()).Returns(Enumerable.Empty<Project>().AsQueryable());
            var validator = new ProjectValidator(projectRepo.Object, userRepo.Object);

            //Act
            var result = validator.Validate(new Project("Teszt projekt", 1));

            //Assert
            Assert.IsEmpty(result);

        }

        [TestCaseSource(nameof(GetInvalidProjectTestData))]
        public void ValidateProjectInvalidProjectTests(List<Project> projects, List<User> users, Project projectToValidate, string expectedErrorMessage)
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
            var validator = new ProjectValidator(projectRepo.Object, userRepo.Object);

            //Act
            var result = validator.Validate(projectToValidate);

            //Assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(expectedErrorMessage, result[0].ErrorMessage);
        }

        #region Utils

        static List<TestCaseData> GetInvalidProjectTestData()
        {
            var testData = new List<TestCaseData>();

            User user1 = new User() { Id = id1, Name = "Teszt Elek", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
            User user2 = new User() { Id = id2, Name = "Másik Teszt Elek", Email = "user2@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user2" };
            List<User> users = new List<User>();
            users.Add(user1);

            Project project1 = new Project() { Id = id1, Name = "Teszt Project 1", OwnerId = user1.Id, Owner = user1 };
            Project project2 = new Project() { Id = id2, Name = "Teszt Project 2", OwnerId = user2.Id, Owner = user2 };
            List<Project> projects = new List<Project>();
            projects.Add(project1);

            testData.Add(new TestCaseData(projects, users, new Project() { Name = project1.Name, OwnerId = project1.OwnerId, Owner = project1.Owner }, $"Project name already exists! Project name: {project1.Name}"));
            testData.Add(new TestCaseData(projects, users, new Project() { Id = project2.Id, Name = project2.Name, OwnerId = project1.OwnerId, Owner = project1.Owner }, $"Project with the given id does not exists! Id: {project2.Id}"));
            testData.Add(new TestCaseData(projects, users, new Project() { Name = project2.Name, OwnerId = project2.OwnerId, Owner = project2.Owner }, $"User does not exist with the given project owner id! Owner id: {project2.OwnerId}"));
            testData.Add(new TestCaseData(projects, users, new Project() { Name = null, OwnerId = id1, Owner = project1.Owner }, "The Name field is required."));
            testData.Add(new TestCaseData(projects, users, new Project() { Name = project2.Name, Owner = project1.Owner }, "The OwnerId field is required."));
            return testData;
        }
        #endregion
    }
}
