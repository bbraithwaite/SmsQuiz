using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.ApiModel.Users
{
    [Serializable]
    public class GetUser
    {
        public Guid ID { get; set; }

        public string Username { get; set; }
    }
}
