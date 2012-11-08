using System.Text.RegularExpressions;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Entrants
{
    /// <summary>
    /// The contact details of an entrant via SMS.
    /// </summary>
    public class SmsContact : EntrantContact, IValidatable
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
                return EntrantContactType.Sms;
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
                return !string.IsNullOrEmpty(Contact) && ValidateNumberFormat(Contact);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsContact" /> class.
        /// </summary>
        public SmsContact() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsContact" /> class.
        /// </summary>
        /// <param name="contact">The contact.</param>
        public SmsContact(string contact) : base(contact) { }

        /// <summary>
        /// Indicates whether the number is 11 digits in length'
        /// </summary>
        /// <param name="contactNumber">The contact number.</param>
        /// <returns>A value indicating whether the number format is valid</returns>
        private static bool ValidateNumberFormat(string contactNumber)
        {
            return new Regex(@"^\d{11}$").IsMatch(RemoveCountryPrefix(contactNumber));
        }

        /// <summary>
        /// Returns the phone number with the +44 UK area code replaced with 0'
        /// </summary>
        /// <param name="contactNumber">The contact number.</param>
        /// <returns>
        /// The contact number with the prefix removed.
        /// </returns>
        private static string RemoveCountryPrefix(string contactNumber)
        {
            return contactNumber.Replace("+44", "0");
        }
    }
}
