using System;
using System.Collections.Generic;
using BB.SmsQuiz.Model.Competitions;

namespace BB.SmsQuiz.Services.Messaging.Competition
{
    [Serializable()]
    public class CompetitionItem
    {
        public Guid ID { get; set; }
        public string Question { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string CompetitionKey { get; set; }
        public string CreatedByName { get; set; }
        public IEnumerable<PossibleAnswerItem> PossibleAnswers { get; set; }
    }

    [Serializable()]
    public class PossibleAnswerItem
    {
        public bool IsCorrectAnswer { get; set; }
        public CompetitionAnswer AnswerKey { get; set; }
        public string AnswerText { get; set; }
    }
}