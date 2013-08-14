// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCompetition.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BB.SmsQuiz.ApiModel.Competitions
{
    /// <summary>
    /// The get competition.
    /// </summary>
    public class GetCompetition
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the closing date.
        /// </summary>
        public DateTime ClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the competition key.
        /// </summary>
        public string CompetitionKey { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the possible answers.
        /// </summary>
        public IEnumerable<PossibleAnswerItem> PossibleAnswers { get; set; }

        /// <summary>
        /// Gets or sets the statistics.
        /// </summary>
        public StatisticsItem Statistics { get; set; }
    }
}