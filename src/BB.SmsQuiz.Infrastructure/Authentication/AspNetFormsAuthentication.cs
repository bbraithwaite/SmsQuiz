using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;

namespace BB.SmsQuiz.Infrastructure.Authentication
{
    public class AspNetFormsAuthentication : IFormsAuthentication
    {
        public void SetAuthenticationToken(string token)
        {
            FormsAuthentication.SetAuthCookie(token, false);
        }

        public string AuthenticationToken
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
