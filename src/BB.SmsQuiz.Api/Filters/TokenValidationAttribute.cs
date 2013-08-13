// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenValidationAttribute.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using BB.SmsQuiz.Api.Controllers;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace BB.SmsQuiz.Api.Filters
{
    /// <summary>
    /// The token validation attribute.
    /// </summary>
    public class TokenValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The on action executing. Validates security token.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                string token = actionContext.Request.Headers.Authorization.ToString();
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