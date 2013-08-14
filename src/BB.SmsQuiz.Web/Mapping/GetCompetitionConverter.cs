using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Mapping
{
    public class GetCompetitionConverter : ITypeConverter<GetCompetition, CompetitionViewModel>
    {
        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="PostCompetition"/>.
        /// </returns>
        public CompetitionViewModel Convert(ResolutionContext context)
        {
            var getCompetition = (GetCompetition)context.SourceValue;
            var item = new CompetitionViewModel()
                {
                    ID = getCompetition.Id.ToString(),
                    ClosingDate = getCompetition.ClosingDate,
                    CompetitionKey = getCompetition.CompetitionKey,
                    CorrectAnswerKey = getCompetition.PossibleAnswers.Single(x => x.IsCorrectAnswer).AnswerKey,
                    Answer1 = getCompetition.PossibleAnswers.ElementAt(0).AnswerText,
                    Answer2 = getCompetition.PossibleAnswers.ElementAt(1).AnswerText,
                    Answer3 = getCompetition.PossibleAnswers.ElementAt(2).AnswerText,
                    Answer4 = getCompetition.PossibleAnswers.ElementAt(3).AnswerText,
                    Question = getCompetition.Question,
                    CreatedDate = getCompetition.CreatedDate,
                    AnswerACount = getCompetition.Statistics.AnswerACount,
                    AnswerBCount = getCompetition.Statistics.AnswerBCount,
                    AnswerCCount = getCompetition.Statistics.AnswerCCount,
                    AnswerDCount = getCompetition.Statistics.AnswerDCount,
                    CreatedBy = getCompetition.CreatedBy,
                    Status = getCompetition.Status
                };

            return item;
        }
    }
}