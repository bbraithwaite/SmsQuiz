using BB.SmsQuiz.Model.Competitions.Entrants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Model.Tests.Entrants
{
    /// <summary>
    /// Tests for validating Email Contact details.
    /// </summary>
    [TestClass]
    public class EmailContactTests
    {
        /// <summary>
        /// A test that verifies an email address must be in the correct format to be valid. 
        /// </summary>
        [TestMethod]
        public void EmailContactWithValidEmailAddressIsValid()
        {
            // Arrange
            EmailContact emailContact = new EmailContact("test@example.com");

            // Act
            bool isValid = emailContact.IsValid;

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// A test that verifies an email address that is not supplied does not pass as being valid.
        /// </summary>
        [TestMethod]
        public void EmptyEmailContactIsNotValid()
        {
            // Arrange
            EmailContact emailContact = new EmailContact("");

            // Act
            bool isValid = emailContact.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// A test that verifies an email address that is not in the correct format does not pass as being valid.
        /// </summary>
        [TestMethod]
        public void EmailContactWithInValidEmailAddressIsNotValid()
        {
            // Arrange
            EmailContact emailContact = new EmailContact("testexample.com");

            // Act
            bool isValid = emailContact.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
