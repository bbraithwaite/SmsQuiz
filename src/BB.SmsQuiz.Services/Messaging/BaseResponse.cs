using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Services.Messaging
{
    public class BaseResponse
    {
        public ResponseStatus Status { get; set; }
        public IEnumerable<ValidationItem> ValidationErrors { get; set; }
    }
}
