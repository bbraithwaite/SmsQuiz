// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;
using System.Web.Mvc;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Web.Infrastructure;

namespace BB.SmsQuiz.Web.Controllers
{
    /// <summary>
    /// The base controller.
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly IBaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public BaseController(IBaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the forms authentication.
        /// </summary>
        public IFormsAuthentication FormsAuthentication
        {
            get { return _context.FormsAuthentication; }
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        public HttpClient Client
        {
            get { return _context.Client; }
        }

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        public IMapper Mapper
        {
            get { return _context.Mapper; }
        }

        /// <summary>
        /// The add model errors.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        public void AddModelErrors(HttpResponseMessage response)
        {
            foreach (var item in response.Content.ReadAsAsync<dynamic>().Result.Items)
            {
                ModelState.AddModelError(item.PropertyName.Value, item.Message.Value);
            }
        }

        /// <summary>
        /// The error view.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        protected ActionResult ErrorView(HttpResponseMessage response)
        {
            ModelState.AddModelError("Model", response.ReasonPhrase);
            return View("Error");
        }
    }
}