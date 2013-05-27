using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.ApiModel.Competitions
{
    public class GetCompetition
    {
        public Guid Id { get; set; }

        public string Question { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ClosingDate { get; set; }

        public string CompetitionKey { get; set; }

        public string CreatedBy { get; set; }

        public string Status { get; set; }

        public IEnumerable<PossibleAnswerItem> PossibleAnswers { get; set; }

        public StatisticsItem Statistics { get; set; }
    }

    public class PossibleAnswerItem
    {
        public bool IsCorrectAnswer { get; set; }

        public int AnswerKey { get; set; }

        public string AnswerText { get; set; }
    }

    public class EntrantItem
    {
        public Guid ID { get; set; }

        public string CompetitionKey { get; set; }
    }

    public class StatisticsItem
    {
        public int CorrectCount { get; set; }

        public int IncorrectCount { get; set; }

        public decimal AnswerAPercentage { get; set; }

        public decimal AnswerBPercentage { get; set; }

        public decimal AnswerCPercentage { get; set; }

        public decimal AnswerDPercentage { get; set; }

        public int AnswerACount { get; set; }

        public int AnswerBCount { get; set; }

        public int AnswerCCount { get; set; }

        public int AnswerDCount { get; set; }
    }
}
