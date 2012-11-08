using System;
using BB.SmsQuiz.Model.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Model.Tests.Competitions
{
    /// <summary>
    /// Tests for Competitions
    /// </summary>
    [TestClass]
    public class CompetitionTests
    {
        /// <summary>
        /// Tests that the competition is valid.
        /// </summary>
        [TestMethod]
        public void CompetitionIsValid()
        {
            // Arrange
            Competition competition = new Competition();
            competition.Question = "What is one plus one?";
            competition.ClosingDate = DateTime.Now.AddMonths(1);
            competition.CompetitionKey = "WINPRIZE";
            competition.CorrectAnswer = CompetitionAnswer.A;

            // Act
            bool isValid = competition.IsValid;

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Test the competition is not valid.
        /// </summary>
        [TestMethod]
        public void CompetitionIsNotValid()
        {
            // Arrange
            Competition competition = new Competition();

            // Act
            bool isValid = competition.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
