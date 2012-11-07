using System;
using System.Data.SqlClient;

namespace BB.SmsQuiz.Model.Entrants
{
    /// <summary>
    /// A competition entrant.
    /// </summary>
    public class Entrant : IValidatable
    {
        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>
        /// The answer.
        /// </value>
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the competition key.
        /// </summary>
        /// <value>
        /// The competition key.
        /// </value>
        public string CompetitionKey { get; set; }

        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        /// <value>
        /// The contact.
        /// </value>
        public EntrantContact Contact { get; set; }

        /// <summary>
        /// Gets or sets the entry date.
        /// </summary>
        /// <value>
        /// The entry date.
        /// </value>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the source e.g. SMS, Email.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public EntrantSource Source { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(Answer) &&
                    !string.IsNullOrEmpty(CompetitionKey) &&
                    EntryDate != DateTime.MinValue &&
                    Source != EntrantSource.NotSet &&
                    Contact.IsValid);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entrant" /> class.
        /// </summary>
        public Entrant()
        {
            this.EntryDate = DateTime.Now;
        }
    }
}
