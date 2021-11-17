using Moq;
using NUnit.Framework;
using OQM10P_HFT_2021221.Models;
using OQM10P_HFT_2021221.Repository.Interfaces;
using OQM10P_HFT_2021221.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Test
{
    [TestFixture]
    public class IssueValidatorTests
    {
        #region
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
            var result = validator.Validate(new Issue() { Title="Teszt issue", ProjectId=project1.Id});

            //Assert
            Assert.IsEmpty(result);
        }

        //[TestCaseSource(nameof(GetInvalidProjectTestData))]
        public void InvalidTests()
        {
            Project project1 = new Project() { Id = id1, Name = "Teszt Project 1", GoalDescription = "Valami cél", OwnerId = id1 };
            List<Project> projects = new List<Project>();
            projects.Add(project1);

            var projectRepo = new Mock<IProjectRepo>();
            projectRepo.Setup(x => x.ReadAll()).Returns(projects.AsQueryable());
            projectRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(projects[0]);

            var issueRepo = new Mock<IIssueRepo>();
            var issue1 = new Issue() { Title = "Teszt feladat 1", Id = id1, ProjectId = project1.Id };

        }



        #region

        static List<TestCaseData> GetInvalidProjectTestData()
        {
            var testData = new List<TestCaseData>();


            return testData;
        }

        #endregion


    }
}
