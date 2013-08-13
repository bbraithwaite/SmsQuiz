// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntrantContact.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Competitions.Entrants
{
    /// <summary>
    /// The abstract class for entrant contact details.
    /// </summary>
    public abstract class EntrantContact : EntityBase, IEntrantContact
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntrantContact"/> class.
        /// </summary>
        /// <param name="contact">
        /// The contact.
        /// </param>
        protected EntrantContact(string contact)
        {
            Contact = contact;
        }

        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        /// <value>
        /// The contact.
        /// </value>
        public string Contact { get; set; }

        /// <summary>
        /// Gets the type of the contact.
        /// </summary>
        /// <value>
        /// The type of the contact.
        /// </value>
        public abstract EntrantContactType ContactType { get; }
    }
}