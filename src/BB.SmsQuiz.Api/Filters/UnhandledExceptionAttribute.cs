// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnhandledExceptionAttribute.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace BB.SmsQuiz.Api.Filters
{
    /// <summary>
    /// The unhandled exception attribute.
    /// </summary>
    public class UnhandledExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}