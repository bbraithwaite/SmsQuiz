using System.Collections.Generic;
using BB.SmsQuiz.ReadModel.Competition;

namespace BB.SmsQuiz.Web.Models
{
    public class CompetitionViewModel
    {
        public IEnumerable<CompetitionItem> Competitions { get; set; }
    }
}