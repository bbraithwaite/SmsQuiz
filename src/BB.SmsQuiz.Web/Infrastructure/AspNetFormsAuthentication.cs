using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BB.SmsQuiz.Web.Infrastructure
{
    public class AspNetFormsAuthentication : IFormsAuthentication
    {
        public void SetAuthenticationToken(string token)
        {
            FormsAuthentication.SetAuthCookie(token, false);
        }

        public string GetAuthenticationToken()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}