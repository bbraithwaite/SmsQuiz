using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.ApiModel.Competitions
{
    public class PostCompetition
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Question { get; set; }

        public string CompetitionKey { get; set; }

        public DateTime ClosingDate { get; set; }

        public string[] Answers { get; set; }

        public int CorrectAnswerKey { get; set; }
    }
}
