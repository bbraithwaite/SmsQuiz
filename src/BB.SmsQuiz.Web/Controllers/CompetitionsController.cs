using System;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using BB.SmsQuiz.Web.Infrastructure;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Controllers
{
    [Authorize]
    public class CompetitionsController : BaseController
    {
        public CompetitionsController(IBaseContext context) : base(context) { }

        //
        // GET: /Competition/
        public ActionResult Index()
        {
            var response = Client.GetAsync("competitions").Result;

            if (response.IsSuccessStatusCode)
            {
                return View(response.Content.ReadAsAsync<dynamic>().Result);
            }

            return ErrorView(response);
        }

        //
        // GET: /Competition/Details/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var response = Client.GetAsync("competitions/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return View(response.Content.ReadAsAsync<dynamic>().Result);
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
            var response = Client.PostAsJsonAsync("competitions", competition).Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    return RedirectToAction("Index");
                case HttpStatusCode.BadRequest:
                    AddModelErrors(response);
                    return View(competition);
            }

            return ErrorView(response);
        }

        //
        // GET: /Competition/Edit/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var response = Client.GetAsync("competitions/" + id).Result;

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
            var response = Client.PutAsJsonAsync("competitions/" + id, competition).Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return RedirectToAction("Index");
                case HttpStatusCode.BadRequest:
                    AddModelErrors(response);

                    return View(competition);
            }

            return ErrorView(response);
        }

        //
        // POST: /Competition/Create
        [HttpPost]
        public ActionResult Close(Guid id)
        {
            var response = Client.PutAsJsonAsync("competitions/" + id + "/close", new {}).Result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return RedirectToAction("Details", new { id });
            }

            return ErrorView(response);
        }

    }
}
