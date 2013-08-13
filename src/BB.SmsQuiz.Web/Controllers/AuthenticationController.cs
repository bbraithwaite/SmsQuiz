// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationController.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using BB.SmsQuiz.Web.Infrastructure;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Controllers
{
    /// <summary>
    /// The authentication controller.
    /// </summary>
    public class AuthenticationController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public AuthenticationController(IBaseContext context) : base(context)
        {
        }

        // GET: /Accounts/Logon
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // POST: /Accounts/Logon
        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
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

        // GET: /Accounts/LogOut
        /// <summary>
        /// The log out.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}