// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionStatus.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BB.SmsQuiz.Model.Competitions.States
{
    /// <summary>
    /// The competition status types
    /// </summary>
    public enum CompetitionStatus : int
    {
        /// <summary>
        /// Default
        /// </summary>
        NotSet = 0, 

        /// <summary>
        /// Open Status
        /// </summary>
        Open = 1, 

        /// <summary>
        /// Closed status
        /// </summary>
        Closed = 2
    }
}