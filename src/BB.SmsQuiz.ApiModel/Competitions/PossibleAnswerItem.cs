// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PossibleAnswerItem.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.ApiModel.Competitions
{
    /// <summary>
    /// The possible answer item.
    /// </summary>
    public class PossibleAnswerItem
    {
        /// <summary>
        /// Gets or sets a value indicating whether is correct answer.
        /// </summary>
        public bool IsCorrectAnswer { get; set; }

        /// <summary>
        /// Gets or sets the answer key.
        /// </summary>
        public int AnswerKey { get; set; }

        /// <summary>
        /// Gets or sets the answer text.
        /// </summary>
        public string AnswerText { get; set; }
    }
}