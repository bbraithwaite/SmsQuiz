using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSTestExtensions;

namespace BB.SmsQuiz.Model.Tests.Competitions
{
    /// <summary>
    /// Tests for Competitions 
    /// </summary>
    [TestClass]
    public class CompetitionTests : BaseTest
    {
        /// <summary>
        /// Tests that the competition is valid.
        /// </summary>
        [TestMethod]
        public void CompetitionIsValid()
        {
            // Arrange
            Competition competition = Stubs.StubCompetition();

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
            var competition = new Competition();

            // Act
            bool isValid = competition.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Test that pick winner is called when expected.
        /// </summary>
        [TestMethod]
        public void PickWinnerCallsCompetitionState()
        {
            // Arrange
            var state = new Mock<ICompetitionState>();
            var competition = new Competition();
            competition.SetCompetitionState(state.Object);

            // Act
            competition.PickWinner();

            // Assert
            state.Verify(s => s.PickWinner(It.IsAny<Competition>()), Times.Once());
        }

        /// <summary>
        /// Test that an exception is thrown when pick winner is called on a closed competition.
        /// </summary>
        [TestMethod]
        public void PickWinnerOnClosedCompetitionThrowsException()
        {
            // Arrange
            var state = new ClosedState();
            var competition = new Competition();
            competition.SetCompetitionState(state);

            // Act
            Assert.Throws<CompetitionClosedException>(competition.PickWinner);
        }
    }
}
