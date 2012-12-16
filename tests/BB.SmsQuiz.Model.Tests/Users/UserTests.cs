using BB.SmsQuiz.Model.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BB.SmsQuiz.Infrastructure.Encryption;
using Moq;

namespace BB.SmsQuiz.Model.Tests.Users
{
    /// <summary>
    /// Unit tests for the User class.
    /// </summary>
    [TestClass]
    public class UserTests
    {
        /// <summary>
        /// Tests that the user is valid.
        /// </summary>
        [TestMethod]
        public void UserIsValid()
        {
            // Arrange
            Mock<IEncryptionService> encryptionService = new Mock<IEncryptionService>();
            encryptionService.Setup(e => e.Encrypt(It.IsAny<string>())).Returns("encrypted");

            User user = new User();
            user.Username = "username";
            user.Password = EncryptedString.Create("password", encryptionService.Object);

            // Act
            bool isValid = user.IsValid;

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests that the user is not valid.
        /// </summary>
        [TestMethod]
        public void UserIsNotValid()
        {
            // Arrange
            User user = new User();

            // Act
            bool isValid = user.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Tests that the user is not valid when the password is blank.
        /// </summary>
        [TestMethod]
        public void UserWithEmptyPasswordIsNotValid()
        {
            // Arrange
            Mock<IEncryptionService> encryptionService = new Mock<IEncryptionService>();
            encryptionService.Setup(e => e.Encrypt(It.IsAny<string>())).Returns(string.Empty);

            User user = new User();
            user.Username = "username";
            user.Password = EncryptedString.Create(string.Empty, encryptionService.Object);

            // Act
            bool isValid = user.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
