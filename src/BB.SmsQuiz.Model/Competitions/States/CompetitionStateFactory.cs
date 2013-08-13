// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionStateFactory.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.Model.Competitions.States
{
    /// <summary>
    /// Factory for creating competition state instance based on status.
    /// </summary>
    public static class CompetitionStateFactory
    {
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <returns>
        /// An instance of the status.
        /// </returns>
        public static ICompetitionState GetInstance(CompetitionStatus status)
        {
            switch (status)
            {
                case CompetitionStatus.Open:
                    return new OpenState();
                case CompetitionStatus.Closed:
                    return new ClosedState();
                default:
                    throw new ArgumentException("status not valid");
            }
        }
    }
}