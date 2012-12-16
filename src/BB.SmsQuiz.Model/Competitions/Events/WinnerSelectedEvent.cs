using BB.SmsQuiz.Infrastructure.Domain.Events;

namespace BB.SmsQuiz.Model.Competitions.Events
{
    /// <summary>
    /// The event raised when a winner has been selected.
    /// </summary>
    public class WinnerSelectedEvent : IDomainEvent
    {
        /// <summary>
        /// Gets or sets the competition.
        /// </summary>
        /// <value>
        /// The competition.
        /// </value>
        public Competition Competition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WinnerSelectedEvent" /> class.
        /// </summary>
        /// <param name="competition">The competition.</param>
        /// <param name="winningEntrant">The winning entrant.</param>
        public WinnerSelectedEvent(Competition competition)
        {
            Competition = competition;
        }
    }
}