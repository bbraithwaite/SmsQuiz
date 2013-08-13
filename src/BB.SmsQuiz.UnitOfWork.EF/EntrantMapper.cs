// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntrantMapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;
using BB.SmsQuiz.Repository.EF.Models;

namespace BB.SmsQuiz.Repository.EF
{
    /// <summary>
    /// The entrant mapper.
    /// </summary>
    internal static class EntrantMapper
    {
        /// <summary>
        /// The map from.
        /// </summary>
        /// <param name="item">
        /// </param>
        /// <returns>
        /// The <see cref="Entrant"/>.
        /// </returns>
        internal static Entrant MapFrom(EntrantData item)
        {
            if (item == null)
            {
                return null;
            }

            var contact = EntrantContactFactory.GetInstance((EntrantContactType)item.ContactType);
            contact.Contact = item.ContactDetail;

            var entrant = new Entrant()
            {
                ID = item.ID,
                Answer = (CompetitionAnswer)item.AnswerKey,
                EntryDate = item.EntryDate,
                Source = (EntrantSource)item.Source,
                Contact = contact
            };

            return entrant;
        }
    }
}