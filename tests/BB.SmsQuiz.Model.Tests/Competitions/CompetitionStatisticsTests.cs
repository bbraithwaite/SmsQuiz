using System.Linq;
using BB.SmsQuiz.Model.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Model.Tests.Competitions
{
    /// <summary>
    /// Tests for the competition report.
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
            Competition competition = Stubs.StubCompetition();

            // Act
            int correctAnswerCount = competition.CorrectEntrants.Count();

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
            Competition competition = Stubs.StubCompetition();

            // Act
            int incorrectAnswerCount = competition.IncorrectEntrants.Count();

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
        /// Percentages the of answers for A is 52.63 percent.
        /// </summary>
        [TestMethod]
        public void CountOfAnswersForAIsCorrect()
        {
            AssertAnswerCount(CompetitionAnswer.A, 10);
        }

        /// <summary>
        /// Percentages the of answers for B is 10.53 percent.
        /// </summary>
        [TestMethod]
        public void CountOfAnswersForBIsCorrect()
        {
            AssertAnswerCount(CompetitionAnswer.B, 2);
        }

        /// <summary>
        /// Percentages the of answers for C is 15.79 percent.
        /// </summary>
        [TestMethod]
        public void CountOfAnswersForCIsCorrect()
        {
            AssertAnswerCount(CompetitionAnswer.C, 3);
        }

        /// <summary>
        /// Percentages the of answers for D is 21.05 percent.
        /// </summary>
        [TestMethod]
        public void CountOfAnswersForDIsCorrect()
        {
            AssertAnswerCount(CompetitionAnswer.D, 4);
        }

        /// <summary>
        /// Asserts the answer percentage.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <param name="expectedPercentage">The expected percentage.</param>
        private static void AssertAnswerPercentage(CompetitionAnswer answer, decimal expectedPercentage)
        {
            // Arrange
            Competition competition = Stubs.StubCompetition();

            // Act
            decimal percentage = competition.GetPercentageOfEntrants(answer);

            // Assert
            Assert.AreEqual(expectedPercentage, decimal.Round(percentage, 2));
        }

        /// <summary>
        /// Asserts the answer count.
        /// </summary>
        /// <param name="answer">The answer.</param>
        /// <param name="expectedCount">The expected count.</param>
        private static void AssertAnswerCount(CompetitionAnswer answer, int expectedCount)
        {
            // Arrange
            Competition competition = Stubs.StubCompetition();

            // Act
            int count = competition.GetNumberOfEntrants(answer);

            // Assert
            Assert.AreEqual(expectedCount, count);
        }
    }
}