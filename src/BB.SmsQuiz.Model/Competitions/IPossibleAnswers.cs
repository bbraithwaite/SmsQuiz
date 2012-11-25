using System;
using System.Collections.Generic;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// Contains the logic for the possible answers attached to a competition.
    /// </summary>
   public interface IPossibleAnswers : IValidatable
    {
        /// <summary>
        /// Adds the specified possible answer.
        /// </summary>
        /// <param name="possibleAnswer">The possible answer.</param>
        void Add(PossibleAnswer possibleAnswer);

        /// <summary>
        /// Gets the answers.
        /// </summary>
        /// <value>
        /// The answers.
        /// </value>
        IEnumerable<PossibleAnswer> Answers { get; }

        /// <summary>
        /// Gets the correct answer.
        /// </summary>
        /// <value>
        /// The correct answer.
        /// </value>
        PossibleAnswer CorrectAnswer { get; }
    }
}
