using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BB.SmsQuiz.Api.Resources
{
    public class BaseController : ApiController
    {
        protected HttpResponseMessage CreatedHttpResponse(object newId, object createdItem)
        {
            var httpResponse = Request.CreateResponse(HttpStatusCode.Created, createdItem);
            string uri = Url.Link("DefaultApi", new { id = newId });
            httpResponse.Headers.Location = new Uri(uri);
            return httpResponse;
        }
    }
}