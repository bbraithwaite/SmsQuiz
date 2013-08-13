// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmsContact.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace BB.SmsQuiz.Model.Competitions.Entrants
{
    /// <summary>
    /// The contact details of an entrant via SMS.
    /// </summary>
    public sealed class SmsContact : EntrantContact
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsContact" /> class.
        /// </summary>
        public SmsContact()
            : base(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsContact"/> class.
        /// </summary>
        /// <param name="contact">
        /// The contact.
        /// </param>
        public SmsContact(string contact)
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
            get { return EntrantContactType.Sms; }
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

            if (!ValidateNumberFormat(Contact))
            {
                ValidationErrors.Add("Contact", "Invalid Phone Number");
            }
        }

        /// <summary>
        /// Indicates whether the number is 11 digits in length'
        /// </summary>
        /// <param name="contactNumber">
        /// The contact number.
        /// </param>
        /// <returns>
        /// A value indicating whether the number format is valid
        /// </returns>
        private static bool ValidateNumberFormat(string contactNumber)
        {
            return new Regex(@"^\d{11}$").IsMatch(RemoveCountryPrefix(contactNumber));
        }

        /// <summary>
        /// Returns the phone number with the +44 UK area code replaced with 0'
        /// </summary>
        /// <param name="contactNumber">
        /// The contact number.
        /// </param>
        /// <returns>
        /// The contact number with the prefix removed.
        /// </returns>
        private static string RemoveCountryPrefix(string contactNumber)
        {
            return contactNumber.Replace("+44", "0");
        }
    }
}