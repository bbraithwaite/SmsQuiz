using System;
using System.Collections.Generic;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Events;
using BB.SmsQuiz.Model.Entrants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;

namespace BB.SmsQuiz.Model.Tests.Competitions
{
    /// <summary>
    /// Tests for the logic of selecting a winner.
    /// </summary>
    [TestClass]
    public class WinnerSelectorTests : BaseTest
    {
        /// <summary>
        /// Tests that selecting a winner raises winner selected event with the expected instance parameters.
        /// </summary>
        [TestMethod]
        public void SelectingAWinnerRaisesWinnerSelectedEvent()
        {
            // Arrange
            WinnerSelector winnerSelector = new WinnerSelector(new CompetitionStatistics(GetMockCompetition(), GetMockEntrants()));
            WinnerSelectedEvent winnerSelectedEvent = null;
            DomainEvents.Register<WinnerSelectedEvent>(evt => winnerSelectedEvent = evt);

            // Act
            winnerSelector.PickWinner();

            // Assert
            Assert.IsNotNull(winnerSelectedEvent);
            Assert.IsNotNull(winnerSelectedEvent.Competition);
        }

        /// <summary>
        /// Test that selecting a winner when there are no correct answers throws an exception.
        /// </summary>
        [TestMethod]
        public void SelectWinnerWithNoCorrectAnswersThrowsNoCorrectAnswerException()
        {
            // Arrange
            WinnerSelector winnerSelector = new WinnerSelector(new CompetitionStatistics(GetMockCompetition(), new List<Entrant>()));

            // Act & Assert
            Assert.Throws<NoCorrectAnswerException>(() => winnerSelector.PickWinner(), "There are no winners for competition: WINPRIZE");
        }

        /// <summary>
        /// Gets the mock entrants.
        /// </summary>
        /// <returns>A mock list of entrants pre-programmed with expectated state.</returns>
        private static IList<Entrant> GetMockEntrants()
        {
            IList<Entrant> entrants = new List<Entrant>();

            // Correct answer
            for (int i = 0; i < 6; i++) {
                entrants.Add(new Entrant() { Answer = CompetitionAnswer.A, EntryDate = new DateTime(2012, 11, 1) });
            }

            // Incorrect answers - B
            for (int i = 0; i < 2; i++)
            {
                entrants.Add(new Entrant() { Answer = CompetitionAnswer.B, EntryDate = new DateTime(2012, 11, 1) });
            }

            // Incorrect answers - C
            for (int i = 0; i < 3; i++)
            {
                entrants.Add(new Entrant() { Answer = CompetitionAnswer.C, EntryDate = new DateTime(2012, 11, 2) });
            }

            // Incorrect answers - D
            for (int i = 0; i < 4; i++)
            {
                entrants.Add(new Entrant() { Answer = CompetitionAnswer.D, EntryDate = new DateTime(2012, 11, 1) });
            }

            return entrants;
        }

        /// <summary>
        /// Gets the mock competition.
        /// </summary>
        /// <returns>A mock competition pre-programmed with expectated state.</returns>
        private static Competition GetMockCompetition()
        {
            Competition competition = new Competition();
            competition.CorrectAnswer = CompetitionAnswer.A;
            competition.ClosingDate = new DateTime(2012, 12, 1);
            competition.CompetitionKey = "WINPRIZE";
            return competition;
        }
    }
}
