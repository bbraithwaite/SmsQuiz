// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Infrastructure.Encryption;

namespace BB.SmsQuiz.Model.Users
{
    /// <summary>
    /// A user with the admin system.
    /// </summary>
    public sealed class User : EntityBase, IAggregateRoot
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public EncryptedString Password { get; set; }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Username))
            {
                ValidationErrors.Add("Username");
            }

            if (Password == null || !Password.EncryptedValue.Any())
            {
                ValidationErrors.Add("Password");
            }
        }
    }
}