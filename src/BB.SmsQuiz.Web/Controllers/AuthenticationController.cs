using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using BB.SmsQuiz.Web.Infrastructure;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IBaseContext context) :base(context) { }

        //
        // GET: /Accounts/Logon
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Accounts/Logon
        [HttpPost]
        public ActionResult Index(AuthenticationViewModel model)
        {
            var result = Client.PostAsJsonAsync("authentication", model).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                FormsAuthentication.SetAuthenticationToken(result.Content.ReadAsAsync<string>().Result);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //
        // GET: /Accounts/LogOut
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
