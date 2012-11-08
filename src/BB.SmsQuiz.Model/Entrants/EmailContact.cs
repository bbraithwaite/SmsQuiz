using System.Net.Mail;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Entrants
{
    /// <summary>
    /// The contact details of an entrant via email.
    /// </summary>
    public class EmailContact : EntrantContact, IValidatable
    {
        /// <summary>
        /// Gets the type of the contact.
        /// </summary>
        /// <value>
        /// The type of the contact.
        /// </value>
        public override EntrantContactType ContactType
        {
            get
            {
                return EntrantContactType.Email;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public override bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Contact) && ValidateEmail(Contact);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailContact" /> class.
        /// </summary>
        public EmailContact() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailContact" /> class.
        /// </summary>
        /// <param name="contact">The contact.</param>
        public EmailContact(string contact) : base(contact) { }

        /// <summary>
        /// Validates the email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns>A value indicating whether the email address is valid.</returns>
        private static bool ValidateEmail(string emailAddress)
        {
            try
            {
                MailAddress address = new MailAddress(emailAddress);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
