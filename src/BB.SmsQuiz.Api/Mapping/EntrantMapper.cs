using System;
using AutoMapper;
using BB.SmsQuiz.ApiModel.EnterCompetition;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;

namespace BB.SmsQuiz.Api.Mapping
{
    public class EntrantItemConverter : ITypeConverter<PostEnterCompetition, Entrant>
    {
        public Entrant Convert(ResolutionContext context)
        {
            var item = (PostEnterCompetition)context.SourceValue;
            var contactDetails =
                EntrantContactFactory.GetInstance(
                    (EntrantContactType)Enum.Parse(typeof(EntrantContactType), item.ContactType));

            contactDetails.Contact = item.Contact;

            var entrant = new Entrant()
            {
                Answer = (CompetitionAnswer)Enum.Parse(typeof(CompetitionAnswer), item.Answer),
                Contact = contactDetails,
                Source = (EntrantSource)Enum.Parse(typeof(EntrantSource), item.Source)
            };

            return entrant;
        }
    }
}