using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BB.SmsQuiz.Api.Controllers.Users;
using BB.SmsQuiz.Model.Competitions;

namespace BB.SmsQuiz.Api.Controllers.Competitions
{
    [Serializable, DataContract]
    public class CompetitionItem
    {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string Question { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime ClosingDate { get; set; }

        [DataMember]
        public string CompetitionKey { get; set; }

        [DataMember]
        public UserItem CreatedBy { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public IEnumerable<PossibleAnswerItem> PossibleAnswers { get; set; }

        [DataMember]
        public StatisticsItem Statistics { get; set; }
    }

    [Serializable, DataContract]
    public class PossibleAnswerItem
    {
        [DataMember]
        public bool IsCorrectAnswer { get; set; }

        [DataMember]
        public CompetitionAnswer AnswerKey { get; set; }

        [DataMember]
        public string AnswerText { get; set; }
    }

    [Serializable, DataContract]
    public class EntrantItem
    {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string CompetitionKey { get; set; }
    }

    [Serializable, DataContract]
    public class StatisticsItem
    {
        [DataMember]
        public int CorrectCount { get; set; }

        [DataMember]
        public int IncorrectCount { get; set; }

        [DataMember]
        public decimal AnswerAPercentage { get; set; }

        [DataMember]
        public decimal AnswerBPercentage { get; set; }

        [DataMember]
        public decimal AnswerCPercentage { get; set; }

        [DataMember]
        public decimal AnswerDPercentage { get; set; }

        [DataMember]
        public int AnswerACount { get; set; }

        [DataMember]
        public int AnswerBCount { get; set; }

        [DataMember]
        public int AnswerCCount { get; set; }

        [DataMember]
        public int AnswerDCount { get; set; }
    }
}