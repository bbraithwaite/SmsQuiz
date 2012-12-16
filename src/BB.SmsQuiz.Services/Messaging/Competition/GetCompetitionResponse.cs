using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Services.Messaging.Competition
{
    public class GetCompetitionResponse : BaseResponse
    {
        public CompetitionItem Competition { get; set; }
    }
}
