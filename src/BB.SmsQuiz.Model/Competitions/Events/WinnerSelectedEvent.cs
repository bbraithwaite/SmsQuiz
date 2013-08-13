// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinnerSelectedEvent.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using BB.SmsQuiz.Infrastructure.Domain.Events;

namespace BB.SmsQuiz.Model.Competitions.Events
{
    /// <summary>
    /// The event raised when a winner has been selected.
    /// </summary>
    public class WinnerSelectedEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WinnerSelectedEvent"/> class.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        public WinnerSelectedEvent(Competition competition)
        {
            Competition = competition;
        }

        /// <summary>
        /// Gets or sets the competition.
        /// </summary>
        /// <value>
        /// The competition.
        /// </value>
        public Competition Competition { get; set; }
    }
}