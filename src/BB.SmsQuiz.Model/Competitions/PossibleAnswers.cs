// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PossibleAnswers.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// Contains the logic for the possible answers attached to a competition.
    /// </summary>
    public class PossibleAnswers : EntityBase
    {
        /// <summary>
        /// The valid number of answers
        /// </summary>
        private const int ValidNumberOfAnswers = 4;

        /// <summary>
        /// The possible answers
        /// </summary>
        private readonly Dictionary<CompetitionAnswer, PossibleAnswer> _possibleAnswers = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PossibleAnswers" /> class.
        /// </summary>
        public PossibleAnswers()
        {
            _possibleAnswers = new Dictionary<CompetitionAnswer, PossibleAnswer>();

            for (int i = 1; i <= ValidNumberOfAnswers; i++)
            {
                var key = (CompetitionAnswer)i;
                _possibleAnswers.Add(key, new PossibleAnswer(key));
            }
        }

        /// <summary>
        /// Gets the answers.
        /// </summary>
        /// <value>
        /// The answers.
        /// </value>
        public IEnumerable<PossibleAnswer> Answers
        {
            get { return _possibleAnswers.Values.ToList(); }
        }

        /// <summary>
        /// Gets the correct answer.
        /// </summary>
        /// <value>
        /// The correct answer.
        /// </value>
        public PossibleAnswer CorrectAnswer
        {
            get { return Answers.SingleOrDefault(a => a.IsCorrectAnswer); }
        }

        /// <summary>
        /// Adds the specified answer.
        /// </summary>
        /// <param name="answer">
        /// The answer.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="isCorrectAnswer">
        /// if set to <c>true</c> [is correct answer].
        /// </param>
        public void Add(CompetitionAnswer answer, string description, bool isCorrectAnswer = false)
        {
            Add(new PossibleAnswer(isCorrectAnswer, answer, description));
        }

        /// <summary>
        /// Adds the specified possible answer.
        /// </summary>
        /// <param name="possibleAnswer">
        /// The possible answer.
        /// </param>
        public void Add(PossibleAnswer possibleAnswer)
        {
            _possibleAnswers[possibleAnswer.AnswerKey] = possibleAnswer;
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        protected override void Validate()
        {
            if (CorrectAnswer == null)
            {
                ValidationErrors.Add("Correct Answer");
            }
        }
    }
}