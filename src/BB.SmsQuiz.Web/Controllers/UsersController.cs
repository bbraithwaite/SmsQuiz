using System;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Controllers
{
    public class UsersController : BaseController
    {
        /// <summary>
        /// The _client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public UsersController(HttpClient client)
        {
            _client = client;
        }

        //
        // GET: /Users
        [HttpGet]
        public ActionResult Index()
        {
            var response = _client.GetAsync("users").Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic users = response.Content.ReadAsAsync<dynamic>().Result;
                return View(users);
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
            var response = _client.PostAsJsonAsync("users", user).Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    return RedirectToAction("Index");
                case HttpStatusCode.BadRequest:
                    foreach (var item in response.Content.ReadAsAsync<dynamic>().Result.Items)
                    {
                        ModelState.AddModelError(item.PropertyName.Value, item.Message.Value);
                    }

                    return View(user);
            }

            return ErrorView(response);
        }

        //
        // GET: /Users/Edit/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var response = _client.GetAsync("users/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic user = response.Content.ReadAsAsync<dynamic>().Result;
                return View(new UserView()
                    {
                        Username = user.Username
                    });
            }

            return ErrorView(response);
        }

        //
        // POST: /Users/Edit/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        [HttpPost]
        public ActionResult Edit(Guid id, UserView user)
        {
            var response = _client.PutAsJsonAsync("users/" + id, user).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            foreach (var item in response.Content.ReadAsAsync<dynamic>().Result.Items)
            {
                ModelState.AddModelError(item.PropertyName.Value, item.Message.Value);
            }

            return View(user);
        }

        //
        // GET: /Users/Delete
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var response = _client.GetAsync("users/" + id).Result;

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
            var response = _client.DeleteAsync("users/" + id).Result;

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return RedirectToAction("Index");
            }

            return ErrorView(response);
        }
    }
}
