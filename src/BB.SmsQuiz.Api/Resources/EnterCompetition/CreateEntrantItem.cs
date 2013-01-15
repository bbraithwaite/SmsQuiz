using System;
using System.Runtime.Serialization;

namespace BB.SmsQuiz.Api.Resources.EnterCompetition
{
    [Serializable, DataContract]
    public class CreateEntrantItem
    {
        [DataMember]
        public string CompetitionKey { get; set; }

        [DataMember]
        public string ContactType { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Answer { get; set; }

        [DataMember]
        public string Contact { get; set; }
    }
}