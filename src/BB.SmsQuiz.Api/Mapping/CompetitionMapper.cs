using System;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.ApiModel.Users;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Users;
using System.Collections.Generic;
using System.Linq;

namespace BB.SmsQuiz.Api.Mapping
{
    public class CompetitionStatisticsConverter : ITypeConverter<Competition, GetCompetition>
    {
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
            item.PossibleAnswers = Mapper.Map<PossibleAnswers, IEnumerable<PossibleAnswerItem>>(competition.PossibleAnswers);
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

    public class CreateCompetitionItemConverter : ITypeConverter<PostCompetition, Competition>
    {
        public Competition Convert(ResolutionContext context)
        {
            var item = (PostCompetition)context.SourceValue;
            var competition = new Competition()
            {
                ID = item.Id,
                Question = item.Question,
                ClosingDate = item.ClosingDate,
                CompetitionKey = item.CompetitionKey,
                CreatedBy = new User() { ID = item.UserId }
            };

            for (int i = 0; i < item.Answers.Count(); i++)
            {
                var key = (CompetitionAnswer)Enum.Parse(typeof(CompetitionAnswer), item.Answers[i]);
                competition.PossibleAnswers.Add(key, item.Answers[i], (int)key == item.CorrectAnswerKey);
            }

            return competition;
        }
    }

    public class PossibleAnswerItemsConverter : ITypeConverter<IEnumerable<PossibleAnswerItem>, PossibleAnswers>
    {
        public PossibleAnswers Convert(ResolutionContext context)
        {
            var from = (IEnumerable<PossibleAnswerItem>)context.SourceValue;
            var pa = new PossibleAnswers();

            for (int i = 0; i < from.Count(); i++)
            {
                PossibleAnswerItem item = from.ElementAt(i);
                pa.Add(new PossibleAnswer(item.IsCorrectAnswer, (CompetitionAnswer)item.AnswerKey, item.AnswerText));
            }

            return pa;
        }
    }

    public class PossibleAnswersConverter : ITypeConverter<PossibleAnswers, IEnumerable<PossibleAnswerItem>>
    {
        public IEnumerable<PossibleAnswerItem> Convert(ResolutionContext context)
        {
            var from = (PossibleAnswers)context.SourceValue;
            return Mapper.Map<IEnumerable<PossibleAnswer>, IEnumerable<PossibleAnswerItem>>(from.Answers);
        }
    }
}