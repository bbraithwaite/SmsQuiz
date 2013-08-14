using AutoMapper;
using BB.SmsQuiz.Api.Mapping;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.ApiModel.EnterCompetition;
using BB.SmsQuiz.ApiModel.Users;
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
            Mapper.CreateMap<Competition, GetCompetition>().ConvertUsing(new CompetitionStatisticsConverter());
            Mapper.CreateMap<User, GetUser>().ConvertUsing(new UserItemConverter());
            Mapper.CreateMap<IEnumerable<User>, IEnumerable<GetUser>>().ConvertUsing(new UserItemsConverter());
            Mapper.CreateMap<PossibleAnswer, PossibleAnswerItem>();
            Mapper.CreateMap<PossibleAnswerItem, PossibleAnswer>();
            Mapper.CreateMap<IEnumerable<PossibleAnswerItem>, PossibleAnswers>().ConvertUsing(new PossibleAnswerItemsConverter());
            Mapper.CreateMap<PossibleAnswers, IEnumerable<PossibleAnswerItem>>().ConvertUsing(new PossibleAnswersConverter());
            Mapper.CreateMap<PostEnterCompetition, Entrant>().ConvertUsing(new EntrantItemConverter());
        }
    }
}