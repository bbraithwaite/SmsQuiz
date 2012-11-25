using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Entrants
{
    /// <summary>
    /// The abstract class for entrant contact details.
    /// </summary>
    public abstract class EntrantContact : EntityBase, IEntrantContact
    {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="EntrantContact" /> class.
        /// </summary>
        /// <param name="contact">The contact.</param>
        protected EntrantContact(string contact)
        {
            Contact = contact;
        }
    }
}
