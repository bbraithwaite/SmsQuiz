// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseCompetitionItemConverter.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Mapping
{
    /// <summary>
    /// The base competition item converter.
    /// </summary>
    public class BaseCompetitionItemConverter : ITypeConverter<BaseCompetition, Competition>
    {
        /// <summary>
        /// Converts base competition properties.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Competition Convert(ResolutionContext context)
        {
            var item = (BaseCompetition)context.SourceValue;
            var competition = new Competition()
                {
                    Question = item.Question, 
                    ClosingDate = item.ClosingDate, 
                    CompetitionKey = item.CompetitionKey, 
                    CreatedBy = new User() {ID = item.UserId}
                };

            for (int i = 0; i < item.Answers.Count(); i++)
            {
                var key = (CompetitionAnswer) Enum.Parse(typeof (CompetitionAnswer), item.Answers[i]);
                competition.PossibleAnswers.Add(key, item.Answers[i], (int) key == item.CorrectAnswerKey);
            }

            return competition;
        }
    }
}