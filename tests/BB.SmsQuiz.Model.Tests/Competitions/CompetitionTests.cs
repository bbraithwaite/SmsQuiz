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
            competition.Question = "Who is Luke Skywalker's father?";
            competition.ClosingDate = DateTime.Now.AddMonths(1);
            competition.CompetitionKey = "WINPRIZE";
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.A, Description = "Darth Vader", IsCorrectAnswer = true });
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.B, Description = "Obi Wan Kenobi" });
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.C, Description = "George Lucas" });
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.D, Description = "Walt Disney" });

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
