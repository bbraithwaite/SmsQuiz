// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Entrant.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Competitions.Entrants
{
    /// <summary>
    /// A competition entrant.
    /// </summary>
    public class Entrant : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entrant" /> class.
        /// </summary>
        public Entrant()
        {
            this.EntryDate = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>
        /// The answer.
        /// </value>
        public CompetitionAnswer Answer { get; set; }

        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        /// <value>
        /// The contact.
        /// </value>
        public IEntrantContact Contact { get; set; }

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
        /// Validates this instance.
        /// </summary>
        protected override void Validate()
        {
            if (Answer == CompetitionAnswer.NotSet)
            {
                ValidationErrors.Add("Answer");
            }

            if (Source == EntrantSource.NotSet)
            {
                ValidationErrors.Add("EntryDate");
            }

            if (!Contact.IsValid)
            {
                ValidationErrors.AddRange(Contact.ValidationErrors.Items);
            }
        }
    }
}