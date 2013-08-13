// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionMapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.ApiModel.Users;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Mapping
{
    /// <summary>
    /// The competition statistics converter.
    /// </summary>
    public class CompetitionStatisticsConverter : ITypeConverter<Competition, GetCompetition>
    {
        /// <summary>
        /// Converts a competition domain entity to the response object (GetCompetition).
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="GetCompetition"/>.
        /// </returns>
        public GetCompetition Convert(ResolutionContext context)
        {
            var competition = (Competition)context.SourceValue;
            var item = new GetCompetition();
            var statistics = new StatisticsItem();

            item.ClosingDate = competition.ClosingDate;
            item.CompetitionKey = competition.CompetitionKey;
            item.CreatedBy = Mapper.Map<User, GetUser>(competition.CreatedBy).Username;
            item.CreatedDate = competition.CreatedDate;
            item.Id = competition.ID;
            item.PossibleAnswers =
                Mapper.Map<PossibleAnswers, IEnumerable<PossibleAnswerItem>>(competition.PossibleAnswers);
            item.Question = competition.Question;
            item.Status = competition.Status.ToString();

            statistics.CorrectCount = competition.CorrectEntrants.Count();
            statistics.IncorrectCount = competition.IncorrectEntrants.Count();
            statistics.AnswerAPercentage = competition.GetPercentageOfEntrants(CompetitionAnswer.A);
            statistics.AnswerBPercentage = competition.GetPercentageOfEntrants(CompetitionAnswer.B);
            statistics.AnswerCPercentage = competition.GetPercentageOfEntrants(CompetitionAnswer.C);
            statistics.AnswerDPercentage = competition.GetPercentageOfEntrants(CompetitionAnswer.D);

            statistics.AnswerACount = competition.GetNumberOfEntrants(CompetitionAnswer.A);
            statistics.AnswerBCount = competition.GetNumberOfEntrants(CompetitionAnswer.B);
            statistics.AnswerCCount = competition.GetNumberOfEntrants(CompetitionAnswer.C);
            statistics.AnswerDCount = competition.GetNumberOfEntrants(CompetitionAnswer.D);

            item.Statistics = statistics;

            return item;
        }
    }
}