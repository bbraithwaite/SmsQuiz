using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Infrastructure.Authentication
{
    public interface ITokenAuthentication
    {
        string Token { get; }
        bool IsValid(string token);
    }
}
