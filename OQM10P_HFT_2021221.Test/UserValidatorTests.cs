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
    public class UserValidatorTests
    {
        #region Constants

        const int id1 = 1;
        const int id2 = 2;

        #endregion

        [Test]
        public void ValidateUserValidTest()
        {
            //Arrange
            var userRepo = new Mock<IUserRepo>();
            userRepo.Setup(x => x.ReadAll()).Returns(Enumerable.Empty<User>().AsQueryable());
            var validator = new UserValidator(userRepo.Object);

            //Act
            var result = validator.Validate(new User { Name = "Teszt Elek", Email = "teszt@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "teszt" });

            //Assert
            Assert.IsEmpty(result);

        }


        [TestCaseSource(nameof(GetInvalidUserTestData))]
        public void ValidateUserInvalidTest(List<User> users, User userToSave, string expectedErrorMessage)
        {
            //Arrange
            var userRepo = new Mock<IUserRepo>();
            userRepo.Setup(x => x.ReadAll()).Returns(users.AsQueryable());
            //userRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(users[0]); //???
            var validator = new UserValidator(userRepo.Object);

            //Act
            var result = validator.Validate(userToSave);

            //Assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(expectedErrorMessage, result[0].ErrorMessage);

        }

        #region
        static List<TestCaseData> GetInvalidUserTestData()
        {
            User user1 = new User() { Id = id1, Name = "Teszt Elek", Email = "user1@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user1" };
            User user2 = new User() { Name = "Másik Teszt Elek", Email = "user2@teszt.com", Position = UserPositionType.JUNIOR_DEV, Sex = UserSexType.MALE, Username = "user2" };
            var testData = new List<TestCaseData>();
            List<User> users = new List<User>();
            users.Add(user1);

            testData.Add(new TestCaseData(users, new User() { Name = user2.Name, Email = user1.Email, Position = user2.Position, Sex = user2.Sex, Username = user2.Username }, $"Email address already exists! Email: {user1.Email}"));
            testData.Add(new TestCaseData(users, new User() { Name = user2.Name, Email = user2.Email, Position = user2.Position, Sex = user2.Sex, Username = user1.Username }, $"Username already exists! Username: {user1.Username}"));
            testData.Add(new TestCaseData(users, new User() { Id = id2, Name = user2.Name, Email = user2.Email, Position = user2.Position, Sex = user2.Sex, Username = user2.Username }, $"User with the given id does not exists! Id: {id2}"));
            testData.Add(new TestCaseData(users, new User() { Name = user2.Name, Position = user2.Position, Sex = user2.Sex, Username = user2.Username }, $"The Email field is required."));
            testData.Add(new TestCaseData(users, new User() { Name = user2.Name, Email = user2.Email, Position = user2.Position, Sex = user2.Sex }, $"The Username field is required."));
            testData.Add(new TestCaseData(users, new User() { Email = user2.Email, Position = user2.Position, Sex = user2.Sex, Username = user2.Username }, $"The Name field is required."));
            return testData;
        }
        #endregion

    }
}
