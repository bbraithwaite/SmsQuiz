using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BB.SmsQuiz.Services.Messaging;

namespace BB.SmsQuiz.Web.Models
{
    public class BaseViewModel
    {
        public IEnumerable<ValidationItem> ValidationErrors { get; set; }
    }
}