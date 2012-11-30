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
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.A, Description = "Darth Vader", IsCorrectAnswer = true });
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.B, Description = "Obi Wan Kenobi" });
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.C, Description = "George Lucas" });
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.D, Description = "Walt Disney" });

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
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.A, Description = "Darth Vader", IsCorrectAnswer = true });
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.B, Description = "Obi Wan Kenobi" });
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.C, Description = "George Lucas" });

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
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.A, Description = "Darth Vader" });
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.B, Description = "Obi Wan Kenobi" });
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.C, Description = "George Lucas" });
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.D, Description = "Walt Disney" });

            // Act
            bool isValid = possibleAnswers.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Addings a duplicate answer throws a duplicate answer exception.
        /// </summary>
        [TestMethod]
        public void AddingADuplicateAnswerThrowsADuplicateAnswerException()
        {
            // Arrange
            PossibleAnswers possibleAnswers = new PossibleAnswers();
            possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.A, Description = "2" });

            // Act & Assert
            Assert.Throws<DuplicateAnswerException>(() => possibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.A }));
        }
    }
}
