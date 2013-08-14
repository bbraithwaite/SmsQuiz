// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PutCompetition.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.ApiModel.Competitions
{
    /// <summary>
    /// The put competition.
    /// </summary>
    public class PutCompetition
    {
        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the closing date.
        /// </summary>
        public DateTime ClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        public string[] Answers { get; set; }

        /// <summary>
        /// Gets or sets the correct answer key.
        /// </summary>
        public int CorrectAnswerKey { get; set; }
    }
}