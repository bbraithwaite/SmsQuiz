using System.Net.Http;
using System.Web.Mvc;
using BB.SmsQuiz.Web.Infrastructure;

namespace BB.SmsQuiz.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IBaseContext _context;

        public BaseController(IBaseContext context)
        {
            _context = context;
        }

        public IFormsAuthentication FormsAuthentication
        {
            get { return _context.FormsAuthentication; }
        }

        public HttpClient Client
        {
            get { return _context.Client; }
        }

        public void AddModelErrors(HttpResponseMessage response)
        {
            foreach (var item in response.Content.ReadAsAsync<dynamic>().Result.Items)
            {
                ModelState.AddModelError(item.PropertyName.Value, item.Message.Value);
            }
        }

        protected ActionResult ErrorView(HttpResponseMessage response)
        {
            ModelState.AddModelError("Model", response.ReasonPhrase);
            return View("Error");
        }
    }
}
