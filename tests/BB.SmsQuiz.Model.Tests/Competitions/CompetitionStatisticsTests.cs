using System;
using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Entrants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Model.Tests.Competitions
{
    /// <summary>
    /// Tests for the compeition report.
    /// </summary>
    [TestClass]
    public class CompetitionStatisticsTests
    {
        /// <summary>
        /// Tests that the number of correct answers for the competition is 10.
        /// </summary>
        [TestMethod]
        public void TheNumberOfCorrectAnswersIs10()
        {
            // Arrange
            CompetitionStatistics statistics = GetCompetitionStatisticsInstance();

            // Act
            int correctAnswerCount = statistics.CorrectAnswers.Count();

            // Assert
            Assert.AreEqual(10, correctAnswerCount);
        }

        /// <summary>
        /// Tests that the number of correct answers for the competition is 2.
        /// </summary>
        [TestMethod]
        public void TheNumberOfIncorrectAnswersIs9()
        {
            // Arrange
            CompetitionStatistics statistics = GetCompetitionStatisticsInstance();

            // Act
            int incorrectAnswerCount = statistics.IncorrectAnswers.Count();

            // Assert
            Assert.AreEqual(9, incorrectAnswerCount);
        }

        /// <summary>
        /// Percentages the of answers for A is 52.63 percent.
        /// </summary>
        [TestMethod]
        public void PercentageOfAnswersForAIsCorrect()
        {
            AssertAnswerPercentage(CompetitionAnswer.A, 52.63m);
        }

        /// <summary>
        /// Percentages the of answers for B is 10.53 percent.
        /// </summary>
        [TestMethod]
        public void PercentageOfAnswersForBIsCorrect()
        {
            AssertAnswerPercentage(CompetitionAnswer.B, 10.53m);
        }

        /// <summary>
        /// Percentages the of answers for C is 15.79 percent.
        /// </summary>
        [TestMethod]
        public void PercentageOfAnswersForCIsCorrect()
        {
            AssertAnswerPercentage(CompetitionAnswer.C, 15.79m);
        }

        /// <summary>
        /// Percentages the of answers for D is 21.05 percent.
        /// </summary>
        [TestMethod]
        public void PercentageOfAnswersForDIsCorrect()
        {
            AssertAnswerPercentage(CompetitionAnswer.D, 21.05m);
        }

        /// <summary>
        /// Asserts the answer percentage.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <param name="expectedPercentage">The expected percentage.</param>
        private static void AssertAnswerPercentage(CompetitionAnswer answer, decimal expectedPercentage)
        {
            // Arrange
            CompetitionStatistics statistics = GetCompetitionStatisticsInstance();

            // Act
            decimal percentage = statistics.GetPercentageOfEntrans(answer);

            // Assert
            Assert.AreEqual(expectedPercentage, decimal.Round(percentage, 2));
        }

        /// <summary>
        /// Gets the competition statistics instance.
        /// </summary>
        /// <returns>A competition statistics instance with mock data.</returns>
        private static CompetitionStatistics GetCompetitionStatisticsInstance()
        {
            return new CompetitionStatistics(GetMockCompetition(), GetMockEntrants());
        }

        /// <summary>
        /// Gets the mock entrants.
        /// </summary>
        /// <returns>A mock list of entrants pre-programmed with expectated state.</returns>
        private static IList<Entrant> GetMockEntrants()
        {
            IList<Entrant> entrants = new List<Entrant>();

            // Correct answers
            for (int i = 0; i < 10; i++)
            {
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

            // Add entrant which is past the entry date.
            entrants.Add(new Entrant() { Answer = CompetitionAnswer.D, EntryDate = new DateTime(2012, 12, 2) });

            return entrants;
        }

        /// <summary>
        /// Gets the mock competition.
        /// </summary>
        /// <returns>A mock competition pre-programmed with expectated state.</returns>
        private static Competition GetMockCompetition() 
        {
            Competition competition = new Competition();
            competition.ClosingDate = new DateTime(2012, 12, 1);
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.A, Description = "Darth Vader", IsCorrectAnswer = true });
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.B, Description = "Obi Wan Kenobi" });
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.C, Description = "George Lucas" });
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.D, Description = "Walt Disney" });

            return competition;
        }
    }
}