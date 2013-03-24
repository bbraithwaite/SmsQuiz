using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Infrastructure.Authentication
{
    public interface ITokenAuthentication
    {
        string GetToken();
        bool IsValid(string token);
    }
}
