using System;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Users
{
    /// <summary>
    /// A user with the admin system.
    /// </summary>
    public sealed class User : EntityBase
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Username))
                ValidationErrors.Add("Username");
        }
    }
}
