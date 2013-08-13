// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotFoundException.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Net;
using System.Web.Http;

namespace BB.SmsQuiz.Api.Filters
{
    /// <summary>
    /// The not found exception.
    /// </summary>
    public class NotFoundException : HttpResponseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException" /> class.
        /// </summary>
        public NotFoundException() : base(HttpStatusCode.NotFound)
        {
        }
    }
}