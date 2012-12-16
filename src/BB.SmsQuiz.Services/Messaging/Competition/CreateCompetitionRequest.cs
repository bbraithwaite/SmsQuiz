using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Services.Messaging.Competition
{
    public class CreateCompetitionRequest : BaseRequest
    {
        public CompetitionItem Competition { get; set; }
    }
}
