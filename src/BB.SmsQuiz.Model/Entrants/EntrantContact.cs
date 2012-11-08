
namespace BB.SmsQuiz.Model.Entrants
{
    /// <summary>
    /// The abstract class for entrant contact details.
    /// </summary>
    public abstract class EntrantContact
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
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsValid { get; }

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
