using System;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using BB.SmsQuiz.Web.Infrastructure;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(IBaseContext context) :base(context) { }

        //
        // GET: /Users
        [HttpGet]
        public ActionResult Index()
        {
            var response = Client.GetAsync("users").Result;

            if (response.IsSuccessStatusCode)
            {
                return View(response.Content.ReadAsAsync<dynamic>().Result);
            }

            return ErrorView(response);
        }

        //
        // GET: /Users/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //
        // GET: /Users/Create
        [HttpPost]
        public ActionResult Create(UserView user)
        {
            var response = Client.PostAsJsonAsync("users", user).Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    return RedirectToAction("Index");
                case HttpStatusCode.BadRequest:
                    AddModelErrors(response);
                    return View(user);
            }

            return ErrorView(response);
        }

        //
        // GET: /Users/Edit/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var response = Client.GetAsync("users/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return View(new UserView()
                    {
                        Username = response.Content.ReadAsAsync<dynamic>().Result.Username
                    });
            }

            return ErrorView(response);
        }

        //
        // POST: /Users/Edit/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        [HttpPost]
        public ActionResult Edit(Guid id, UserView user)
        {
            var response = Client.PutAsJsonAsync("users/" + id, user).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            AddModelErrors(response);

            return View(user);
        }

        //
        // GET: /Users/Delete
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var response = Client.GetAsync("users/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic user = response.Content.ReadAsAsync<dynamic>().Result;
                return View(user);
            }

            return ErrorView(response);
        }

        //
        // POST: /Users/Delete/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        [HttpPost]
        public ActionResult Delete(Guid id, UserView userView)
        {
            var response = Client.DeleteAsync("users/" + id).Result;

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return RedirectToAction("Index");
            }

            return ErrorView(response);
        }
    }
}
