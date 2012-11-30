using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Model.Competitions.States
{
    /// <summary>
    /// Competition interface required for competition state objects.
    /// </summary>
    public interface ICompetitionState
    {
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        CompetitionStatus Status { get; }

        /// <summary>
        /// Picks the winner.
        /// </summary>
        /// <param name="competition">The competition.</param>
        void PickWinner(Competition competition);
    }
}
