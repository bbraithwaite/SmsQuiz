using AutoMapper;
using BB.SmsQuiz.Api.Mapping;
using BB.SmsQuiz.Api.Controllers.Competitions;
using BB.SmsQuiz.Api.Controllers.EnterCompetition;
using BB.SmsQuiz.Api.Controllers.Users;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;
using BB.SmsQuiz.Model.Users;
using System.Collections.Generic;

namespace BB.SmsQuiz.Api.App_Start
{
    public class AutoMapperBootStrapper
    {
        public static void Configure()
        {
            Mapper.CreateMap<Competition, CompetitionItem>().ConvertUsing(new CompetitionStatisticsConverter());
            Mapper.CreateMap<CreateCompetitionItem, Competition>().ConvertUsing(new CreateCompetitionItemConverter());
            Mapper.CreateMap<User, UserItem>().ConvertUsing(new UserItemConverter());
            Mapper.CreateMap<IEnumerable<User>, IEnumerable<UserItem>>().ConvertUsing(new UserItemsConverter());
            Mapper.CreateMap<PossibleAnswer, PossibleAnswerItem>();
            Mapper.CreateMap<PossibleAnswerItem, PossibleAnswer>();
            Mapper.CreateMap<IEnumerable<PossibleAnswerItem>, PossibleAnswers>().ConvertUsing(new PossibleAnswerItemsConverter());
            Mapper.CreateMap<PossibleAnswers, IEnumerable<PossibleAnswerItem>>().ConvertUsing(new PossibleAnswersConverter());
            Mapper.CreateMap<CreateEntrantItem, Entrant>().ConvertUsing(new EntrantItemConverter());
        }
    }
}