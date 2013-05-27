using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using BB.SmsQuiz.Api.Controllers;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace BB.SmsQuiz.Api.Filters
{
    public class TokenValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                string token =actionContext.Request.Headers.Authorization.ToString();
                var controller = (BaseController)actionContext.ControllerContext.Controller;

                if (controller.TokenAuthentication.IsValid(token))
                {
                    base.OnActionExecuting(actionContext);
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent("Unauthorized Request")
                    };
                }
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Missing Authorization Token")
                };
            }
        }
    }
}