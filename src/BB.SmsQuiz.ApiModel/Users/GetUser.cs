// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetUser.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   The get user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.ApiModel.Users
{
    /// <summary>
    /// The get user.
    /// </summary>
    [Serializable]
    public class GetUser
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }
    }
}