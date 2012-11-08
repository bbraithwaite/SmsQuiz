using BB.SmsQuiz.Model.Entrants;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// The competition winner.
    /// </summary>
    public class CompetitionWinner
    {
        /// <summary>
        /// Gets or sets the entrant.
        /// </summary>
        /// <value>
        /// The entrant.
        /// </value>
        public Entrant Entrant { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the is prized claimed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is prized claimed; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrizedClaimed { get; set; }
    }
}
