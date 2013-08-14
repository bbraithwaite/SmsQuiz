// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntrantItem.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.ApiModel.Competitions
{
    /// <summary>
    /// The entrant item.
    /// </summary>
    public class EntrantItem
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the competition key.
        /// </summary>
        public string CompetitionKey { get; set; }
    }
}