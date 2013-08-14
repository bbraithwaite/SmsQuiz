// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationViewModel.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.Web.Models
{
    /// <summary>
    /// The authentication view model.
    /// </summary>
    public class AuthenticationViewModel
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