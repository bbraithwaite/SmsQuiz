using System;

namespace BB.SmsQuiz.Api.Controllers.Competitions
{
    public class CreateCompetitionItem
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string Question { get; set; }
        public string CompetitionKey { get; set; }
        public DateTime ClosingDate { get; set; }
        public string[] Answers { get; set; }
        public string CorrectAnswerKey { get; set; }
    }
}