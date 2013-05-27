using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.ApiModel.EnterCompetition
{
    public class PostEnterCompetition
    {
        public string CompetitionKey { get; set; }

        public string ContactType { get; set; }

        public string Source { get; set; }

        public string Answer { get; set; }

        public string Contact { get; set; }
    }
}
