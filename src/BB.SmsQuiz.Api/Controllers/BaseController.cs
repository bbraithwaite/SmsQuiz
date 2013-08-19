// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BB.SmsQuiz.Infrastructure.Authentication;
using Ninject;

namespace BB.SmsQuiz.Api.Controllers
{
    /// <summary>
    /// The base controller.
    /// </summary>
    public class BaseController : ApiController
    {
        /// <summary>
        /// Gets or sets the token authentication.
        /// </summary>
        [Inject]
        public ITokenAuthentication TokenAuthentication { get; set; }

        /// <summary>
        /// Gets the request token.
        /// </summary>
        public string RequestToken
        {
            get
            {
                return ControllerContext.Request.Headers.Authorization.ToString();
            }
        }

        /// <summary>
        /// The created http response.
        /// </summary>
        /// <param name="newId">
        /// The new id.
        /// </param>
        /// <param name="createdItem">
        /// The created item.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        protected HttpResponseMessage CreatedHttpResponse(object newId, object createdItem)
        {
            var httpResponse = Request.CreateResponse(HttpStatusCode.Created, createdItem);
            string uri = Url.Link("DefaultApi", new { id = newId });
            httpResponse.Headers.Location = new Uri(uri);
            return httpResponse;
        }
    }
}