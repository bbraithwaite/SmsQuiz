using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BB.SmsQuiz.Model.Competitions.States;

namespace BB.SmsQuiz.Services.Messaging.Competition
{
    public class GetCompetitionsRequest : BaseRequest
    {
        public int PageIndex { get; set; }
        public CompetitionStatus Status { get; set; }
        public string CompetitionKey { get; set; }
    }
}
