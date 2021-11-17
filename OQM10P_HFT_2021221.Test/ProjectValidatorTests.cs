
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

        /// <summary>
        /// Ez alatt van TestCaseSource-os megoldás, de az valamiért nem működik, ha leveszem róla a kommentezést, akkor az osztály egyik tesztje sem fut le -.-
        /// </summary>
        [Test]
        public void ValidateProjectInvalidProjectNameExistsTest()
        {
            //Arrange
            User user1 = new User() { Id = 1, Name = "Teszt Elek", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
            List<User> users = new List<User>();
            users.Add(user1);
            var userRepo = new Mock<IUserRepo>();
            userRepo.Setup(x => x.ReadAll()).Returns(users.AsQueryable());
            userRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(user1);

            Project project1 = new Project() { Id = id1, Name = "Teszt Project 1", GoalDescription = "Valami cél", OwnerId = id1 };
            Project project2 = new Project() { Id = id2, Name = "Teszt Project 2", GoalDescription = "Valami másik cél", OwnerId = id2 };
            List<Project> projects = new List<Project>();
            projects.Add(project1);

            var projectRepo = new Mock<IProjectRepo>();
            projectRepo.Setup(x => x.ReadAll()).Returns(projects.AsQueryable());
            projectRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(projects[0]);
            var validator = new ProjectValidator(projectRepo.Object, userRepo.Object);

            //Act
            var result = validator.Validate(new Project() { Name = project1.Name, GoalDescription = project2.GoalDescription, OwnerId = id1 });

            //Assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual($"Project name already exists! Email: {project1.Name}", result[0].ErrorMessage);

        }

        /// <summary>
        /// ?????
        /// Valamiért ha ez nincs kikommentezve, akkor az osztály egyik tesztje sem fut.
        /// Mármint nem ír semmi hibát, egyszerűen csak nem futnak le. Ugyanez a felállás a UserValidatorTests-ben működik..
        /// </summary>
        /// <returns></returns>
        //[TestCaseSource(nameof(GetInvalidProjectTestData))]
        //public void ValidateProjectInvalidTest(List<Project> projects, Project projectToSave, string expectedErrorMessage)
        //{
        //    //Arrange

        //    User user1 = new User() { Id = id1, Name = "Teszt Elek", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
        //    List<User> users = new List<User>();
        //    users.Add(user1);

        //    var userRepo = new Mock<IUserRepo>();
        //    userRepo.Setup(x => x.ReadAll()).Returns(users.AsQueryable());
        //    userRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(user1);



        //    var projectRepo = new Mock<IProjectRepo>();
        //    projectRepo.Setup(x => x.ReadAll()).Returns(projects.AsQueryable());
        //    projectRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(projects[0]);
        //    var validator = new ProjectValidator(projectRepo.Object, userRepo.Object);

        //    //Act
        //    var result = validator.Validate(projectToSave);

        //    //Assert
        //    Assert.IsNotEmpty(result);
        //    Assert.AreEqual(1, result.Count());
        //    Assert.AreEqual(expectedErrorMessage, result[0].ErrorMessage);
        //}

        #region

        static List<TestCaseData> GetInvalidProjectTestData()
        {
            var testData = new List<TestCaseData>();

            Project project1 = new Project() { Id = id1, Name = "Teszt Project 1", GoalDescription = "Valami cél", OwnerId = id1 };
            Project project2 = new Project() { Id = id2, Name = "Teszt Project 2", GoalDescription = "Valami másik cél", OwnerId = id2 };
            List<Project> projects = new List<Project>();
            projects.Add(project1);

            testData.Add(new TestCaseData(projects, new Project() { Name = project1.Name, GoalDescription = project2.GoalDescription, OwnerId = id1 }, $"Project name already exists! Email: {project1.Name}"));
            testData.Add(new TestCaseData(projects, new Project() { Id = project2.Id, Name = project1.Name, GoalDescription = project2.GoalDescription, OwnerId = project1.OwnerId }, $"Project with the given id does not exists! Id: {project2.Id}"));
            testData.Add(new TestCaseData(projects, new Project() { Name = project1.Name, GoalDescription = project2.GoalDescription, OwnerId = project2.OwnerId }, $"User does not exist with the given project owner id! Owner id: {project2.OwnerId}"));
            testData.Add(new TestCaseData(projects, new Project() { Name = project1.Name, GoalDescription = project2.GoalDescription, OwnerId = id1 }, $"The Name field is required."));
            return testData;
        }
        #endregion
    }
}
