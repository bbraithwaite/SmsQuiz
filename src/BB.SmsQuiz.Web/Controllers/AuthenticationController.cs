using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using BB.SmsQuiz.Web.Infrastructure;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly HttpClient _client;
        private readonly IFormsAuthentication _formsAuthentication;

        public AuthenticationController(HttpClient client, IFormsAuthentication formsAuthentication)
        {
            _client = client;
            _formsAuthentication = formsAuthentication;
        }

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
            if (_client.PostAsJsonAsync("authentication", model).Result.StatusCode == HttpStatusCode.OK)
            {
                _formsAuthentication.SetAuthenticationToken(model.Username);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //
        // GET: /Accounts/LogOut
        public ActionResult LogOut()
        {
            _formsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
