using System;
using System.Runtime.Serialization;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// An exception where there are no correct answers for a competition.
    /// </summary>
    [Serializable]
    public class NoCorrectAnswerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoCorrectAnswerException" /> class.
        /// </summary>
        public NoCorrectAnswerException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoCorrectAnswerException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public NoCorrectAnswerException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoCorrectAnswerException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public NoCorrectAnswerException(string message, Exception exception)
            : base(message, exception)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoCorrectAnswerException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected NoCorrectAnswerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoCorrectAnswerException" /> class.
        /// </summary>
        /// <param name="competition">The competition.</param>
        public NoCorrectAnswerException(Competition competition)
            : base(string.Format("There are no winners for competition: {0}", competition.CompetitionKey))
        {
        }
    }
}
