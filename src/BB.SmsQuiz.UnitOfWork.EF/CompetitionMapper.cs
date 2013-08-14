// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionMapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using BB.SmsQuiz.Repository.EF.Models;
using BB.SmsQuiz.UnitOfWork.EF;

namespace BB.SmsQuiz.Repository.EF
{
    /// <summary>
    /// The competition mapper.
    /// </summary>
    internal static class CompetitionMapper
    {
        /// <summary>
        /// The map from.
        /// </summary>
        /// <param name="userData">
        /// The user data.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        internal static IEnumerable<Competition> MapFrom(IEnumerable<CompetitionData> userData)
        {
            return userData.Select(MapFrom);
        }

        /// <summary>
        /// The map from.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        internal static Competition MapFrom(CompetitionData data)
        {
            if (data == null)
            {
                return null;
            }

            var item = new Competition()
                {
                    ID = data.ID, 
                    ClosingDate = data.ClosingDate, 
                    CompetitionKey = data.CompetitionKey, 
                    CreatedDate = data.CreatedDate, 
                    Question = data.Question, 
                    CreatedBy = UserMapper.MapFrom(data.User)
                };

            item.SetCompetitionState(CompetitionStateFactory.GetInstance((CompetitionStatus) data.Status));

            if (data.PossibleAnswers != null)
            {
                foreach (var m in data.PossibleAnswers)
                {
                    item.PossibleAnswers.Add(new PossibleAnswer(m.IsCorrectAnswer, (CompetitionAnswer)m.AnswerKey, m.AnswerText));
                }
            }

            if (data.Entrants != null)
            {
                foreach (var entrant in data.Entrants)
                {
                    var mapped = EntrantMapper.MapFrom(entrant);
                    item.AddEntrant(mapped);
                }
            }

            return item;
        }

        /// <summary>
        /// The map to.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        internal static CompetitionData MapTo(Competition item)
        {
            if (item == null)
            {
                return null;
            }

            var data = new CompetitionData
                {
                    ID = item.ID,
                    ClosingDate = item.ClosingDate,
                    CompetitionKey = item.CompetitionKey,
                    CreatedDate = item.CreatedDate,
                    Question = item.Question,
                    CreatedByID = item.CreatedBy.ID,
                    Status = (byte)item.State.Status
                };

            MapPossibleAnswers(item, data);
            MapEntrants(item, data);

            return data;
        }

        /// <summary>
        /// The map entrants.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        private static void MapEntrants(Competition item, CompetitionData data)
        {
            if (item.Entrants != null)
            {
                foreach (var e in item.Entrants.Where(e => e.ID == Guid.Empty))
                {
                    var ed = new EntrantData()
                        {
                            AnswerKey = (int) e.Answer, 
                            EntryDate = e.EntryDate, 
                            Source = (int) e.Source, 
                            ContactType = (int) e.Contact.ContactType, 
                            ContactDetail = e.Contact.Contact, 
                            CompetitionID = item.ID
                        };

                    data.Entrants.Add(ed);
                }
            }
        }

        /// <summary>
        /// The map possible answers.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        private static void MapPossibleAnswers(Competition item, CompetitionData data)
        {
            if (item.PossibleAnswers != null)
            {
                foreach (var pa in item.PossibleAnswers.Answers)
                {
                    var rowItem = data.PossibleAnswers.SingleOrDefault(x => x.AnswerKey == (byte) pa.AnswerKey);

                    bool newRecord = rowItem == null;

                    if (newRecord)
                    {
                        rowItem = new PossibleAnswerData();
                    }

                    rowItem.CompetitionID = item.ID;
                    rowItem.AnswerKey = (byte) pa.AnswerKey;
                    rowItem.AnswerText = pa.AnswerText;
                    rowItem.IsCorrectAnswer = pa.IsCorrectAnswer;

                    if (newRecord)
                    {
                        data.PossibleAnswers.Add(rowItem);
                    }
                }
            }
        }
    }
}