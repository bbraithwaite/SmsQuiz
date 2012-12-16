using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BB.SmsQuiz.Services.Messaging.Competition;
using BB.SmsQuiz.Services.Messaging;

namespace BB.SmsQuiz.Web.Models
{
    public class CompetitionViewModel : BaseViewModel
    {
        public CompetitionItem Competition { get; set; }
    }
}