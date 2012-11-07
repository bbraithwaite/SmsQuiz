using BB.SmsQuiz.Model.Entrants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Model.Tests.Entrants
{
    [TestClass]
    public class EntrantTests
    {
        /// <summary>
        /// Tests for validating entrants.
        /// </summary>
        [TestMethod]
        public void EntrantIsValid()
        {
            // Arrange
            Entrant entrant = new Entrant();
            entrant.Answer = "A";
            entrant.CompetitionKey = "WINPRIZE";
            entrant.Source = EntrantSource.Sms;
            entrant.Contact = new SmsContact("02345612345");

            // Act
            bool isValid = entrant.IsValid;

            // Assert
            Assert.IsTrue(isValid);            
        }
    }
}
