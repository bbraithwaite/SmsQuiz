// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailContact.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Mail;

namespace BB.SmsQuiz.Model.Competitions.Entrants
{
    /// <summary>
    /// The contact details of an entrant via email.
    /// </summary>
    public sealed class EmailContact : EntrantContact
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailContact" /> class.
        /// </summary>
        public EmailContact()
            : base(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailContact"/> class.
        /// </summary>
        /// <param name="contact">
        /// The contact.
        /// </param>
        public EmailContact(string contact)
            : base(contact)
        {
        }

        /// <summary>
        /// Gets the type of the contact.
        /// </summary>
        /// <value>
        /// The type of the contact.
        /// </value>
        public override EntrantContactType ContactType
        {
            get { return EntrantContactType.Email; }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Contact))
            {
                ValidationErrors.Add("Contact");
            }

            if (!ValidateEmail(Contact))
            {
                ValidationErrors.Add("Contact", "Invalid Email Address");
            }
        }

        /// <summary>
        /// Validates the email.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// A value indicating whether the email address is valid.
        /// </returns>
        private static bool ValidateEmail(string emailAddress)
        {
            try
            {
                var address = new MailAddress(emailAddress);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}