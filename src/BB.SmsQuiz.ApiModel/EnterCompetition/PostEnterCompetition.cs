// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostEnterCompetition.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.ApiModel.EnterCompetition
{
    /// <summary>
    /// The post enter competition.
    /// </summary>
    public class PostEnterCompetition
    {
        /// <summary>
        /// Gets or sets the competition key.
        /// </summary>
        public string CompetitionKey { get; set; }

        /// <summary>
        /// Gets or sets the contact type.
        /// </summary>
        public string ContactType { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        public string Contact { get; set; }
    }
}