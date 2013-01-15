using System.Web.Mvc;

namespace BB.SmsQuiz.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}
