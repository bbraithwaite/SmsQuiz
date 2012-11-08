using System.Linq;
using System.Collections.Generic;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// Contains the logic for the possible answers attached to a competition.
    /// </summary>
    public class PossibleAnswers : IValidatable
    {
        /// <summary>
        /// The valid number of answers
        /// </summary>
        private const int ValidNumberOfAnswers = 4;

        /// <summary>
        /// The possible answers
        /// </summary>
        private List<PossibleAnswer> _possibleAnswers = null;

        /// <summary>
        /// Gets the answers.
        /// </summary>
        /// <value>
        /// The answers.
        /// </value>
        public IEnumerable<PossibleAnswer> Answers
        {
            get
            {
                return _possibleAnswers;
            }
        }

        /// <summary>
        /// Gets the correct answer.
        /// </summary>
        /// <value>
        /// The correct answer.
        /// </value>
        public PossibleAnswer CorrectAnswer
        {
            get
            {
                return Answers.SingleOrDefault(a => a.IsCorrectAnswer);
            }
        }

        /// <summary>
        /// Adds the specified possible answer.
        /// </summary>
        /// <param name="possibleAnswer">The possible answer.</param>
        /// <exception cref="DuplicateAnswerException"></exception>
        public void Add(PossibleAnswer possibleAnswer)
        {
            if (_possibleAnswers.Exists(p => p.Answer == possibleAnswer.Answer))
                throw new DuplicateAnswerException();

            _possibleAnswers.Add(possibleAnswer);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get 
            {
                return Answers.Count() == ValidNumberOfAnswers && CorrectAnswer != null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PossibleAnswers" /> class.
        /// </summary>
        public PossibleAnswers()
        {
            _possibleAnswers = new List<PossibleAnswer>();
        }
    }
}
