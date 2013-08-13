// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntrantContact.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Competitions.Entrants
{
    /// <summary>
    /// The EntrantContact interface.
    /// </summary>
    public interface IEntrantContact : IValidatable
    {
        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        /// <value>
        /// The contact.
        /// </value>
        string Contact { get; set; }

        /// <summary>
        /// Gets the type of the contact.
        /// </summary>
        /// <value>
        /// The type of the contact.
        /// </value>
        EntrantContactType ContactType { get; }
    }
}