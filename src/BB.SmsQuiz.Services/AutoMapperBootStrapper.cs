using AutoMapper;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;
using BB.SmsQuiz.Services.Messaging;
using BB.SmsQuiz.Services.Messaging.Competition;
using System.Collections.Generic;

namespace BB.SmsQuiz.Services
{
    public class AutoMapperBootStrapper
    {
        public static void Configure()
        {
            Mapper.CreateMap<Competition, CompetitionItem>();
            Mapper.CreateMap<CompetitionItem, Competition>();
            Mapper.CreateMap<Entrant, EntrantItem>();
            Mapper.CreateMap<ValidationError, ValidationItem>();
            Mapper.CreateMap<PossibleAnswer, PossibleAnswerItem>();
            Mapper.CreateMap<PossibleAnswerItem, PossibleAnswer>();
            Mapper.CreateMap<PossibleAnswers, IEnumerable<PossibleAnswerItem>>().ConvertUsing(new PossibleAnswersConverter());
        }

        public class PossibleAnswersConverter : ITypeConverter<PossibleAnswers, IEnumerable<PossibleAnswerItem>>
        {
            public IEnumerable<PossibleAnswerItem> Convert(ResolutionContext context)
            {
                PossibleAnswers from = (PossibleAnswers)context.SourceValue;
                return Mapper.Map<IEnumerable<PossibleAnswer>, IEnumerable<PossibleAnswerItem>>(from.Answers);
            }
        }
    }
}
