using System;
using System.Collections.Generic;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
            Mock<IPossibleAnswers> answers = new Mock<IPossibleAnswers>();
            answers.Setup(a => a.IsValid).Returns(true);

            Competition competition = new Competition(answers.Object, null);
            competition.Question = "Who is Luke Skywalker's father?";
            competition.ClosingDate = DateTime.Now.AddMonths(1);
            competition.CompetitionKey = "WINPRIZE";

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
            Mock<IPossibleAnswers> answers = new Mock<IPossibleAnswers>();
            answers.Setup(a => a.IsValid).Returns(false);
            answers.Setup(a => a.ValidationErrors).Returns(new ValidationErrors());

            Competition competition = new Competition(answers.Object, null);

            // Act
            bool isValid = competition.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Test the competition is not valid.
        /// </summary>
        [TestMethod]
        public void PickWinnerCallsCompetitionState()
        {
            // Arrange
            Mock<ICompetitionState> state = new Mock<ICompetitionState>();
            Competition competition = new Competition(null, state.Object);

            // Act
            competition.PickWinner();

            // Assert
            state.Verify(s => s.PickWinner(It.IsAny<Competition>()), Times.Once());
        }
    }
}
