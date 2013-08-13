// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionAnswer.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// The possible answers to a competition.
    /// </summary>
    public enum CompetitionAnswer : int
    {
        /// <summary>
        /// The default value
        /// </summary>
        NotSet = 0, 

        /// <summary>
        /// The answer A
        /// </summary>
        A = 1, 

        /// <summary>
        /// The answer B
        /// </summary>
        B = 2, 

        /// <summary>
        /// The answer C
        /// </summary>
        C = 3, 

        /// <summary>
        /// The answer D
        /// </summary>
        D = 4
    }
}