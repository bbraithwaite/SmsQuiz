using System.Web.Http;

namespace BB.SmsQuiz.Api.App_Start
{
    public class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("API Default", "{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}