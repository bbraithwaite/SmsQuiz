using System;
using BB.SmsQuiz.Infrastructure.Domain.Events;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Events;
using BB.SmsQuiz.Model.Competitions.States;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Model.Tests.Competitions
{
    /// <summary>
    ///     Competition state tests
    /// </summary>
    [TestClass]
    public class CompetitionStateTests : BaseTest
    {
        /// <summary>
        ///     Picks the winner, raises the selected winner event and sets the competition status to be closed.
        /// </summary>
        [TestMethod]
        public void PickWinnerWithCorrectAnswers()
        {
            // Arrange
            Competition competition = Stubs.StubCompetition(includeCorrectAnswers: true);
            var competitionState = new OpenState();
            WinnerSelectedEvent winnerSelectedEvent = null;
            DomainEvents.Register<WinnerSelectedEvent>(evt => winnerSelectedEvent = evt);

            // Act
            competitionState.PickWinner(competition);

            // Assert
            Assert.IsNotNull(winnerSelectedEvent.Competition.Winner);
            Assert.AreEqual(CompetitionStatus.Closed, competition.State.Status);
        }

        /// <summary>
        ///     Picks the winner, raises the selected winner event and sets the competition status to be closed.
        /// </summary>
        [TestMethod]
        public void PickWinnerWithNoCorrectAnswers()
        {
            // Arrange
            Competition competition = Stubs.StubCompetition(includeCorrectAnswers: false);
            var competitionState = new OpenState();
            WinnerSelectedEvent winnerSelectedEvent = null;
            DomainEvents.Register<WinnerSelectedEvent>(evt => winnerSelectedEvent = evt);

            // Act
            competitionState.PickWinner(competition);

            // Assert
            Assert.IsNull(winnerSelectedEvent.Competition.Winner);
            Assert.AreEqual(CompetitionStatus.Closed, competition.State.Status);
        }

        /// <summary>
        ///     Picks the winner when the close date has not passed does not raise even or close competition.
        /// </summary>
        [TestMethod]
        public void PickWinnerWhenTheCloseDateHasNotPassed()
        {
            // Arrange
            Competition competition = Stubs.StubCompetition(includeCorrectAnswers: false);
            competition.ClosingDate = DateTime.Now.AddMonths(1); // always ensure the data is in the future

            var competitionState = new OpenState();
            WinnerSelectedEvent winnerSelectedEvent = null;
            DomainEvents.Register<WinnerSelectedEvent>(evt => winnerSelectedEvent = evt);

            // Act
            competitionState.PickWinner(competition);

            // Assert
            Assert.IsNull(winnerSelectedEvent);
            Assert.AreEqual(CompetitionStatus.Open, competition.State.Status);
        }

        /// <summary>
        ///     Throws exception when pick winner is called on a competition in a closed state.
        /// </summary>
        [TestMethod]
        public void PickWinnerWhenACompetitionIsClosedThrowsException()
        {
            // Arrange
            Competition competition = Stubs.StubCompetition(includeCorrectAnswers: true);
            var competitionState = new ClosedState();

            // Act & Assert
            Assert.Throws<CompetitionClosedException>(() => competitionState.PickWinner(competition));
        }
    }
}