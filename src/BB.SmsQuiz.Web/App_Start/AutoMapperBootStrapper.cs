// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperBootStrapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.ApiModel.Users;
using BB.SmsQuiz.Web.Mapping;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.App_Start
{
    /// <summary>
    /// The auto mapper boot strapper.
    /// </summary>
    public class AutoMapperBootStrapper
    {
        /// <summary>
        /// The configure.
        /// </summary>
        public static void Configure()
        {
            Mapper.CreateMap<UserView, GetUser>();
            Mapper.CreateMap<GetUser, UserView>();
            Mapper.CreateMap<UserView, PostUser>();
            Mapper.CreateMap<UserView, PutUser>();
            Mapper.CreateMap<CompetitionViewModel, PostCompetition>().ConvertUsing(new PostCompetitionConverter());
            Mapper.CreateMap<CompetitionViewModel, PutCompetition>().ConvertUsing(new PutCompetitionConverter());
            Mapper.CreateMap<GetCompetition, CompetitionViewModel>().ConvertUsing(new GetCompetitionConverter());
        }
    }
}