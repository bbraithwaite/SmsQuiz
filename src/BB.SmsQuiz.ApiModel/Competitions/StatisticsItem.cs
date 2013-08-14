// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatisticsItem.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.ApiModel.Competitions
{
    /// <summary>
    /// The statistics item.
    /// </summary>
    public class StatisticsItem
    {
        /// <summary>
        /// Gets or sets the correct count.
        /// </summary>
        public int CorrectCount { get; set; }

        /// <summary>
        /// Gets or sets the incorrect count.
        /// </summary>
        public int IncorrectCount { get; set; }

        /// <summary>
        /// Gets or sets the answer a percentage.
        /// </summary>
        public decimal AnswerAPercentage { get; set; }

        /// <summary>
        /// Gets or sets the answer b percentage.
        /// </summary>
        public decimal AnswerBPercentage { get; set; }

        /// <summary>
        /// Gets or sets the answer c percentage.
        /// </summary>
        public decimal AnswerCPercentage { get; set; }

        /// <summary>
        /// Gets or sets the answer d percentage.
        /// </summary>
        public decimal AnswerDPercentage { get; set; }

        /// <summary>
        /// Gets or sets the answer a count.
        /// </summary>
        public int AnswerACount { get; set; }

        /// <summary>
        /// Gets or sets the answer b count.
        /// </summary>
        public int AnswerBCount { get; set; }

        /// <summary>
        /// Gets or sets the answer c count.
        /// </summary>
        public int AnswerCCount { get; set; }

        /// <summary>
        /// Gets or sets the answer d count.
        /// </summary>
        public int AnswerDCount { get; set; }
    }
}