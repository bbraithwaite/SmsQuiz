using System.Web.Http;
using BB.SmsQuiz.Api.Filters;

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
            configuration.Routes.MapHttpRoute(
                name: "EnterCompetition",
                routeTemplate: "competitions/enter",
                defaults: new
                {
                    controller = "EnterCompetition"
                }
            );

            configuration.Routes.MapHttpRoute(
                name: "CloseCompetition",
                routeTemplate: "competitions/close/{id}",
                defaults: new
                {
                    controller = "CloseCompetition"
                }
            );

            configuration.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}",
                new { id = RouteParameter.Optional });

            configuration.Filters.Add(new UnhandledExceptionAttribute());
        }
    }
}