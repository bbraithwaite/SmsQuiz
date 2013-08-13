// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenState.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Infrastructure.Domain.Events;
using BB.SmsQuiz.Model.Competitions.Events;

namespace BB.SmsQuiz.Model.Competitions.States
{
    /// <summary>
    /// The open competition state.
    /// </summary>
    public sealed class OpenState : CompetitionState
    {
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public override CompetitionStatus Status
        {
            get { return CompetitionStatus.Open; }
        }

        /// <summary>
        /// Picks the winner.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        public override void PickWinner(Competition competition)
        {
            if (competition.ClosingDateHasPassed)
            {
                if (competition.HasCorrectEntrants)
                {
                    competition.Winner = competition.CorrectEntrants.SelectRandom();
                }

                DomainEvents.Raise(new WinnerSelectedEvent(competition));
                competition.SetCompetitionState(new ClosedState());
            }
        }
    }
}