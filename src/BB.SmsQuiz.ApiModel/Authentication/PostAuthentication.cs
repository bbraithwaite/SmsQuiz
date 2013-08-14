// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostAuthentication.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.ApiModel.Authentication
{
    /// <summary>
    /// The post authentication.
    /// </summary>
    public class PostAuthentication
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}