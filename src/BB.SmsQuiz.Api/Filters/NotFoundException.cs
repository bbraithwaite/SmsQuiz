using System.Net;
using System.Web.Http;

namespace BB.SmsQuiz.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class NotFoundException : HttpResponseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException" /> class.
        /// </summary>
        public NotFoundException() : base(HttpStatusCode.NotFound) { }
    }
}