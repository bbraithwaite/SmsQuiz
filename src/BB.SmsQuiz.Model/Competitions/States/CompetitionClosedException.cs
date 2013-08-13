// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionClosedException.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace BB.SmsQuiz.Model.Competitions.States
{
    /// <summary>
    /// An exception thrown when an attempt to pick a winner for an already closed competition is made.
    /// </summary>
    [Serializable]
    public sealed class CompetitionClosedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionClosedException" /> class.
        /// </summary>
        public CompetitionClosedException() : base("This competition has already been closed.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionClosedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public CompetitionClosedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionClosedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public CompetitionClosedException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionClosedException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        private CompetitionClosedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}