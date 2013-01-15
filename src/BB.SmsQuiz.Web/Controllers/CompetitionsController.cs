using System;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Controllers
{
    public class CompetitionsController : BaseController
    {
        /// <summary>
        /// The _client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionsController" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public CompetitionsController(HttpClient client)
        {
            _client = client;
        }

        //
        // GET: /Competition/
        public ActionResult Index()
        {
            var response = _client.GetAsync("competitions").Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic users = response.Content.ReadAsAsync<dynamic>().Result;
                return View(users);
            }

            return ErrorView(response);
        }

        //
        // GET: /Competition/Details/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var response = _client.GetAsync("competitions/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic competition = response.Content.ReadAsAsync<dynamic>().Result;
                return View(competition);
            }

            return ErrorView(response);
        }

        //
        // GET: /Competition/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CompetitionViewModel());
        }

        //
        // POST: /Competition/Create
        [HttpPost]
        public ActionResult Create(CompetitionViewModel competition)
        {
            var response = _client.PostAsJsonAsync("competitions", competition).Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    return RedirectToAction("Index");
                case HttpStatusCode.BadRequest:
                    foreach (var item in response.Content.ReadAsAsync<dynamic>().Result.Items)
                    {
                        ModelState.AddModelError(item.PropertyName.Value, item.Message.Value);
                    }

                    return View(competition);
            }

            return ErrorView(response);
        }

        //
        // GET: /Competition/Edit/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var response = _client.GetAsync("competitions/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic competition = response.Content.ReadAsAsync<dynamic>().Result;
                var viewModel = new CompetitionViewModel()
                {
                    ClosingDate = competition.ClosingDate,
                    CompetitionKey = competition.CompetitionKey,
                    ID = competition.ID,
                    Question = competition.Question
                };

                for (int i = 0; i < competition.PossibleAnswers.Count; i++)
                {
                    var pa = competition.PossibleAnswers[i];
                    viewModel.Answers.Add(pa.AnswerText.ToString());

                    if (pa.IsCorrectAnswer.Value)
                    {
                        viewModel.CorrectAnswer = i;
                    }
                }

                return View(viewModel);
            }

            return ErrorView(response);
        }

        //
        // POST: /Competition/Edit/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        [HttpPost]
        public ActionResult Edit(Guid id, CompetitionViewModel competition)
        {
            var response = _client.PutAsJsonAsync("competitions/" + id, competition).Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return RedirectToAction("Index");
                case HttpStatusCode.BadRequest:
                    foreach (var item in response.Content.ReadAsAsync<dynamic>().Result.Items)
                    {
                        ModelState.AddModelError(item.PropertyName.Value, item.Message.Value);
                    }

                    return View(competition);
            }

            return ErrorView(response);
        }
    }
}
