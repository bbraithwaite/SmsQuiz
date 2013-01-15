using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BB.SmsQuiz.Api.Resources.EnterCompetition;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;

namespace BB.SmsQuiz.Api.Mapping
{
    public class EntrantItemConverter : ITypeConverter<CreateEntrantItem, Entrant>
    {
        public Entrant Convert(ResolutionContext context)
        {
            var item = (CreateEntrantItem)context.SourceValue;
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