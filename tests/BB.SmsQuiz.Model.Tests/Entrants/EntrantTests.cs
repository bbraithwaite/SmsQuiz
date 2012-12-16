using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Tests.Entrants
{
    /// <summary>
    /// Tests for the entrant class.
    /// </summary>
    [TestClass]
    public class EntrantTests
    {
        /// <summary>
        /// Entrants the is valid.
        /// </summary>
        [TestMethod]
        public void EntrantIsValid()
        {
            // Arrange
            Mock<IEntrantContact> contact = new Mock<IEntrantContact>();
            contact.Setup(c => c.IsValid).Returns(true);

            Entrant entrant = new Entrant();
            entrant.Answer = CompetitionAnswer.A;
            entrant.Source = EntrantSource.Sms;
            entrant.Contact = contact.Object;

            // Act
            bool isValid = entrant.IsValid;

            // Assert
            Assert.IsTrue(isValid);            
        }

        /// <summary>
        /// Entrants the is not valid.
        /// </summary>
        [TestMethod]
        public void EntrantIsNotValid()
        {
            // Arrange
            Mock<IEntrantContact> contact = new Mock<IEntrantContact>();
            contact.Setup(a => a.ValidationErrors).Returns(new ValidationErrors());

            Entrant entrant = new Entrant();
            entrant.Contact = contact.Object;

            // Act
            bool isValid = entrant.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Entrants the with contact validation errors is not valid.
        /// </summary>
        [TestMethod]
        public void EntrantWithContactValidationErrorsIsNotValid()
        {
            // Arrange
            Mock<IEntrantContact> contact = new Mock<IEntrantContact>();
            ValidationErrors errors = new ValidationErrors();
            errors.Add("invalidProperty");
            contact.Setup(a => a.ValidationErrors).Returns(errors);
            contact.Setup(a => a.IsValid).Returns(false);

            Entrant entrant = new Entrant();
            entrant.Answer = CompetitionAnswer.A;
            entrant.Source = EntrantSource.Sms;
            entrant.Contact = contact.Object;
            
            // Act
            bool isValid = entrant.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
