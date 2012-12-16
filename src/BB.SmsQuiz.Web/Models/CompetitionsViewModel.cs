using System.Collections.Generic;
using BB.SmsQuiz.Services.Messaging.Competition;

namespace BB.SmsQuiz.Web.Models
{
    public class CompetitionsViewModel : BaseViewModel
    {
        public IEnumerable<CompetitionItem> Competitions { get; set; }
    }
}