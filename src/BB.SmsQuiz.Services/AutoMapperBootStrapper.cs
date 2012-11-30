using AutoMapper;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.ReadModel.Competition;

namespace BB.SmsQuiz.Services
{
    public class AutoMapperBootStrapper
    {
        public static void Configure()
        {
            Mapper.CreateMap<Competition, CompetitionItem>();
        }
    }
}
