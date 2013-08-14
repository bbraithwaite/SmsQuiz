// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostCompetition.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.ApiModel.Competitions
{
    /// <summary>
    /// The post competition.
    /// </summary>
    public class PostCompetition
    {
        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the competition key.
        /// </summary>
        public string CompetitionKey { get; set; }

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