using System.Web.Mvc;
using BB.SmsQuiz.Web.Infrastructure;

namespace BB.SmsQuiz.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IBaseContext context) : base(context) { }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}
