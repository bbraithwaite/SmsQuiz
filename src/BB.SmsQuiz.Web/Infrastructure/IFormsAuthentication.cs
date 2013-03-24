using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Web.Infrastructure
{
    public interface IFormsAuthentication
    {
        void SetAuthenticationToken(string token);
        string GetAuthenticationToken();
        void SignOut();
    }
}
