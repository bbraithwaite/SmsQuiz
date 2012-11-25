
namespace BB.SmsQuiz.Model.Competitions.States
{
    /// <summary>
    /// The closed competition state.
    /// </summary>
    public sealed class ClosedState : CompetitionState
    {
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public override CompetitionStatus Status
        {
            get
            {
                return CompetitionStatus.Closed;
            }
        }

        /// <summary>
        /// Picks the winner.
        /// </summary>
        /// <param name="competition">The competition.</param>
        /// <exception cref="BB.SmsQuiz.Model.Competitions.States.CompetitionClosedException"></exception>
        public override void PickWinner(Competition competition)
        {
            throw new CompetitionClosedException();
        }
    }
}
