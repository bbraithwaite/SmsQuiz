using System;
using System.Runtime.Serialization;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// Duplicate Answer exception thrown when an attempt is made to add the same answer key to a list of answers.
    /// </summary>
    [Serializable]
    public sealed class DuplicateAnswerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateAnswerException" /> class.
        /// </summary>
        public DuplicateAnswerException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateAnswerException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DuplicateAnswerException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateAnswerException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public DuplicateAnswerException(string message, Exception exception)
            : base(message, exception)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateAnswerException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        private DuplicateAnswerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateAnswerException" /> class.
        /// </summary>
        /// <param name="possibleAnswer">The possible answer.</param>
        public DuplicateAnswerException(PossibleAnswer possibleAnswer)
            : base(string.Format("An answer with this key has already been added: {0}", possibleAnswer.Answer.ToString()))
        {
        }
    }
}
