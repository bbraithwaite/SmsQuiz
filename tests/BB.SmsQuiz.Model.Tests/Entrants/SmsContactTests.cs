using BB.SmsQuiz.Model.Competitions.Entrants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Model.Tests.Entrants
{
    /// <summary>
    /// Tests for validating SMS Contact details.
    /// </summary>
    [TestClass()]
    public class SmsContactTests
    {
        /// <summary>
        /// A test that verifies a number must be all digits and 11 digits in length to be valid. 
        /// </summary>
        [TestMethod()]
        public void SmsContactIsValidWithAllDigitsNumber()
        {
            // Arrange
            SmsContact smsContact = new SmsContact("02345612345");
            
            // Act
            bool isValid = smsContact.IsValid;

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// A test that verifies a number with the area code prefix must originate from the UK and be 11 digits in length to be valid. 
        /// </summary>
        [TestMethod()]
        public void SmsContactIsValidWithCountryCodePrefixNumber()
        {
            // Arrange
            SmsContact smsContact = new SmsContact("+442345612345");

            // Act
            bool isValid = smsContact.IsValid;

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// A test that verifies a number that is not in the correct format does not pass as being valid.
        /// </summary>
        [TestMethod()]
        public void SmsContactIsNotValidWithAnInvalidNumber()
        {
            // Arrange
            SmsContact smsContact = new SmsContact("12345");

            // Act
            bool isValid = smsContact.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// A test that verifies an empty number does not pass as being valid.
        /// </summary>
        [TestMethod()]
        public void EmptySmsContactIsNotValid()
        {
            // Arrange
            SmsContact smsContact = new SmsContact(string.Empty);

            // Act
            bool isValid = smsContact.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
