using BB.SmsQuiz.Model.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            User user = new User();
            user.Username = "username";
            user.Password = "password";

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
    }
}
