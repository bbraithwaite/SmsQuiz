using System;
using AutoMapper;
using BB.SmsQuiz.Api.Resources.Competitions;
using BB.SmsQuiz.Api.Resources.Users;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Users;
using System.Collections.Generic;
using System.Linq;

namespace BB.SmsQuiz.Api.Mapping
{
    public class CompetitionStatisticsConverter : ITypeConverter<Competition, CompetitionItem>
    {
        public CompetitionItem Convert(ResolutionContext context)
        {
            var competition = (Competition)context.SourceValue;
            var item = new CompetitionItem();
            var statistics = new StatisticsItem();

            item.ClosingDate = competition.ClosingDate;
            item.CompetitionKey = competition.CompetitionKey;
            item.CreatedBy = Mapper.Map<User, UserItem>(competition.CreatedBy);
            item.CreatedDate = competition.CreatedDate;
            item.ID = competition.ID;
            item.PossibleAnswers = Mapper.Map<PossibleAnswers, IEnumerable<PossibleAnswerItem>>(competition.PossibleAnswers);
            item.Question = competition.Question;

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

    public class CreateCompetitionItemConverter : ITypeConverter<CreateCompetitionItem, Competition>
    {
        public Competition Convert(ResolutionContext context)
        {
            var item = (CreateCompetitionItem)context.SourceValue;
            var competition = new Competition()
            {
                ID = item.ID,
                Question = item.Question,
                ClosingDate = item.ClosingDate,
                CompetitionKey = item.CompetitionKey,
                CreatedBy = new User() { ID = item.UserID }
            };

            for (int i = 0; i < item.Answers.Count(); i++)
            {
                var key = (CompetitionAnswer)Enum.Parse(typeof(CompetitionAnswer), item.Answers[i]);
                competition.PossibleAnswers.Add(key, item.Answers[i], key.ToString() == item.CorrectAnswerKey);
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
                pa.Add(new PossibleAnswer(item.IsCorrectAnswer, item.AnswerKey, item.AnswerText));
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