using System.Web.Mvc;
using BB.SmsQuiz.Services;
using BB.SmsQuiz.Web.Models;
using System;
using BB.SmsQuiz.Services.Messaging.Competition;
using BB.SmsQuiz.Services.Messaging;
using System.Collections.Generic;
using BB.SmsQuiz.Model.Competitions;

namespace BB.SmsQuiz.Web.Controllers
{
    /// <summary>
    /// The competition controller.
    /// </summary>
    public class CompetitionsController : Controller
    {
        /// <summary>
        /// The _competition service
        /// </summary>
        ICompetitionService _competitionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionsController" /> class.
        /// </summary>
        /// <param name="competitionService">The competition service.</param>
        public CompetitionsController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        //
        // GET: /Competition/
        public ActionResult Index()
        {
            var request = new GetCompetitionsRequest();
            GetCompetitionsResponse response = _competitionService.GetCompetitions(request);
            var viewModel = new CompetitionsViewModel();
            viewModel.Competitions = response.Competitions;
            return View("Index", viewModel);
        }

        //
        // GET: /Competition/Details/8F05AE9E-3167-4C49-8EF6-B2F9056345B9
        public ActionResult Details(Guid id)
        {
            return GetCompetitionResult(id, "Details");
        }

        //
        // GET: /Competition/Create/
        public ActionResult Create()
        {
            var model = new CompetitionViewModel();
            var answers = new List<PossibleAnswerItem>();

            for (int i = 1; i <= 4; i++)
			{
                answers.Add(new PossibleAnswerItem() { AnswerKey = (CompetitionAnswer)i, IsCorrectAnswer = (i == 1) });
			}

            model.Competition = new CompetitionItem();
            model.Competition.PossibleAnswers = answers;
            return View("Create", model);
        }

        //
        // POST: /Competition/Create
        [HttpPost]
        public ActionResult Create(CompetitionViewModel model)
        {
            var request = new CreateCompetitionRequest();
            request.Competition = model.Competition;
            CreateCompetitionResponse response = _competitionService.CreateCompetition(request);

            if (response.Status == ResponseStatus.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                model.ValidationErrors = response.ValidationErrors;
                return View("Create", model);
            }
        }

        //
        // GET: /Competition/Edit/8F05AE9E-3167-4C49-8EF6-B2F9056345B9
        public ActionResult Edit(Guid id)
        {
            return GetCompetitionResult(id, "Edit");
        }

        //
        // POST: /Competition/Edit/8F05AE9E-3167-4C49-8EF6-B2F9056345B9
        [HttpPost]
        public ActionResult Edit(Guid id, CompetitionViewModel model)
        {
            var request = new CreateCompetitionRequest();
            request.Competition = model.Competition;

            CreateCompetitionResponse response = _competitionService.CreateCompetition(request);

            if (response.Status == ResponseStatus.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                model.ValidationErrors = response.ValidationErrors;
                return View("Edit", model);
            }
        }

        //
        // GET: /Competition/Delete/8F05AE9E-3167-4C49-8EF6-B2F9056345B9
        public ActionResult Delete(Guid id)
        {
            return GetCompetitionResult(id, "Delete");
        }

        //
        // POST: /Competition/Delete/8F05AE9E-3167-4C49-8EF6-B2F9056345B9
        [HttpPost]
        public ActionResult Delete(Guid id, CompetitionViewModel model)
        {
            var request = new DeleteCompetitionRequest();
            request.ID = id;
            DeleteCompetitionResponse response = _competitionService.DeleteCompetition(request);

            if (response.Status == ResponseStatus.OK)
            {
                return RedirectToAction("Index");
            }

            return View("Delete", model);
        }

        /// <summary>
        /// Gets the competition view model.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns the view model.</returns>
        private ActionResult GetCompetitionResult(Guid id, string viewName)
        {
            var request = new GetCompetitionRequest() { ID = id };
            GetCompetitionResponse response = _competitionService.GetCompetition(request);

            if (response.Status == ResponseStatus.OK)
            {
                var viewModel = new CompetitionViewModel();
                viewModel.Competition = response.Competition;
                return View(viewName, viewModel);
            }
            else
            {
                // TODO: change to custom "not found" redirect.
                throw new Exception();
            }
        }
    }
}
