using System.Net.Http;
using System.Web.Mvc;

namespace BB.SmsQuiz.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ActionResult ErrorView(HttpResponseMessage response)
        {
            ModelState.AddModelError("Model", response.ReasonPhrase);
            return View("Error");
        }
    }
}
