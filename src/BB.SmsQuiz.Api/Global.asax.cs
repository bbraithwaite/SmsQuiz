using System.Web.Http;
using BB.SmsQuiz.Api.App_Start;

namespace BB.SmsQuiz.Api
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            AutoMapperBootStrapper.Configure();
        }
    }
}