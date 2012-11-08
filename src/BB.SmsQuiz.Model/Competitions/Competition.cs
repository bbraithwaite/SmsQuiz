using System;
using BB.SmsQuiz.Model.Users;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// A competition.
    /// </summary>
    public class Competition : IValidatable
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>
        /// The question.
        /// </value>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the correct answer.
        /// </summary>
        /// <value>
        /// The correct answer.
        /// </value>
        public CompetitionAnswer CorrectAnswer { get; set; }

        /// <summary>
        /// Gets or sets the competition key.
        /// </summary>
        /// <value>
        /// The competition key.
        /// </value>
        public string CompetitionKey { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public User CreatedBy { get; set; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Gets or sets the closing date.
        /// </summary>
        /// <value>
        /// The closing date.
        /// </value>
        public DateTime ClosingDate { get; set; }

        /// <summary>
        /// Gets the winner.
        /// </summary>
        /// <value>
        /// The winner.
        /// </value>
        public CompetitionWinner Winner { get; internal set; }

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
                return !string.IsNullOrEmpty(Question) &&
                       !string.IsNullOrEmpty(CompetitionKey) &&
                       ClosingDate != DateTime.MinValue &&
                       CorrectAnswer != CompetitionAnswer.NotSet;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Competition" /> class.
        /// </summary>
        public Competition()
        {
            CreationDate = DateTime.Now;
        }
    }
}
