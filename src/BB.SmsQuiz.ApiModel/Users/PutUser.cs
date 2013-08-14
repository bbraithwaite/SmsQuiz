// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PutUser.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   The put user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.ApiModel.Users
{
    /// <summary>
    /// The put user.
    /// </summary>
    [Serializable]
    public class PutUser
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }
    }
}