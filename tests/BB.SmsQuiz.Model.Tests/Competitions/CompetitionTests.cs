using System;
using System.Collections.Generic;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
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
            StubPossibleAnswers answers = new StubPossibleAnswers();
            answers.SetValidState(true);

            Competition competition = new Competition(answers, null);
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
            StubPossibleAnswers answers = new StubPossibleAnswers();
            Competition competition = new Competition(answers, null);

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
            StubCompetitionState state = new StubCompetitionState();
            Competition competition = new Competition(null, state);

            // Act
            competition.PickWinner();

            // Assert
            Assert.AreEqual(1, state.CallCount);
        }

        /// <summary>
        /// Stub of possible answers
        /// </summary>
        private class StubPossibleAnswers : IPossibleAnswers
        {
            /// <summary>
            /// Gets the answers.
            /// </summary>
            /// <value>
            /// The answers.
            /// </value>
            /// <exception cref="System.NotImplementedException"></exception>
            public IEnumerable<PossibleAnswer> Answers
            {
                get { throw new NotImplementedException(); }
            }

            /// <summary>
            /// Gets the correct answer.
            /// </summary>
            /// <value>
            /// The correct answer.
            /// </value>
            /// <exception cref="System.NotImplementedException"></exception>
            public PossibleAnswer CorrectAnswer
            {
                get { throw new NotImplementedException(); }
            }

            /// <summary>
            /// Adds the specified possible answer.
            /// </summary>
            /// <param name="possibleAnswer">The possible answer.</param>
            /// <exception cref="System.NotImplementedException"></exception>
            public void Add(PossibleAnswer possibleAnswer)
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Gets the validation errors.
            /// </summary>
            /// <value>
            /// The validation errors.
            /// </value>
            public ValidationErrors ValidationErrors
            {
                get 
                {
                    if (IsValid)
                    {
                        return new ValidationErrors();
                    }
                    else
                    {
                        // add one row to put this in an invalid state
                        ValidationErrors errors = new ValidationErrors();
                        errors.Add(new ValidationError("property", "required"));
                        return errors;
                    }
                }
            }

            /// <summary>
            /// Gets a value indicating whether this instance is valid.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
            /// </value>
            public bool IsValid { get; private set; }

            /// <summary>
            /// Sets the state of the valid.
            /// </summary>
            /// <param name="isValid">if set to <c>true</c> [is valid].</param>
            public void SetValidState(bool isValid)
            {
                this.IsValid = isValid;
            }
        }

        /// <summary>
        /// Stub of competiton state object.
        /// </summary>
        private class StubCompetitionState : ICompetitionState
        {
            /// <summary>
            /// Gets or sets the call count.
            /// </summary>
            /// <value>
            /// The call count.
            /// </value>
            public int CallCount { get; private set; }

            /// <summary>
            /// Gets the status.
            /// </summary>
            /// <value>
            /// The status.
            /// </value>
            public CompetitionStatus Status { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="StubCompetitionState" /> class.
            /// </summary>
            public StubCompetitionState()
            {
                CallCount = 0; 
            }

            /// <summary>
            /// Picks the winner.
            /// </summary>
            /// <param name="competition">The competition.</param>
            public void PickWinner(Competition competition)
            {
                CallCount++;
            }
        }
    }
}
