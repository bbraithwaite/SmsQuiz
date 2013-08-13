// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionState.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BB.SmsQuiz.Model.Competitions.States
{
    /// <summary>
    /// Competition abstract class required for competition state objects.
    /// </summary>
    public abstract class CompetitionState : ICompetitionState
    {
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public abstract CompetitionStatus Status { get; }

        /// <summary>
        /// Picks the winner.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        public abstract void PickWinner(Competition competition);
    }
}