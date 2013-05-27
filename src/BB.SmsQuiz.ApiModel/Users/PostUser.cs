using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.ApiModel.Users
{
    [Serializable]
    public class PostUser
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
