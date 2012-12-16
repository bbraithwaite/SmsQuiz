using BB.SmsQuiz.Model.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;

namespace BB.SmsQuiz.Model.Tests.Competitions
{
    /// <summary>
    /// Tests for the possible list of answers for a competition.
    /// </summary>
    [TestClass]
    public class PossibleAnswersTests : BaseTest
    {
        /// <summary>
        /// Tests that the possible answers has four entries, A, B, C and D, has only one correct answer and no duplicate answers.
        /// </summary>
        [TestMethod]
        public void PossibleAnswersIsValid()
        {
            // Arrange
            PossibleAnswers possibleAnswers = new PossibleAnswers();
            possibleAnswers.Add(CompetitionAnswer.A, "Darth Vader", isCorrectAnswer: true);
            possibleAnswers.Add(CompetitionAnswer.B, "Obi Wan Kenobi");
            possibleAnswers.Add(CompetitionAnswer.C, "George Lucas");
            possibleAnswers.Add(CompetitionAnswer.D, "Walt Disney");

            // Act
            bool isValid = possibleAnswers.IsValid;

            // Assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Possibles the answers has less than four answers is not valid.
        /// </summary>
        [TestMethod]
        public void PossibleAnswersHasLessThanFourAnswersIsNotValid()
        {
            // Arrange
            PossibleAnswers possibleAnswers = new PossibleAnswers();
            possibleAnswers.Add(CompetitionAnswer.A, "Darth Vader", isCorrectAnswer: true);
            possibleAnswers.Add(CompetitionAnswer.B, "Obi Wan Kenobi");
            possibleAnswers.Add(CompetitionAnswer.C, "George Lucas");

            // Act
            bool isValid = possibleAnswers.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void NoAnswerSetAsCorrectIsNotValid()
        {
            // Arrange
            PossibleAnswers possibleAnswers = new PossibleAnswers();
            possibleAnswers.Add(CompetitionAnswer.A, "Darth Vader");
            possibleAnswers.Add(CompetitionAnswer.B, "Obi Wan Kenobi");
            possibleAnswers.Add(CompetitionAnswer.C, "George Lucas");
            possibleAnswers.Add(CompetitionAnswer.D, "Walt Disney");

            // Act
            bool isValid = possibleAnswers.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
