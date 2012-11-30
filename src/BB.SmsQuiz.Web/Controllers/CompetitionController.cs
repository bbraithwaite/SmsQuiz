using System.Web.Mvc;
using BB.SmsQuiz.Services;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.Controllers
{
    /// <summary>
    /// The competition controller.
    /// </summary>
    public class CompetitionController : Controller
    {
        /// <summary>
        /// The _competition service
        /// </summary>
        ICompetitionService _competitionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionController" /> class.
        /// </summary>
        /// <param name="competitionService">The competition service.</param>
        public CompetitionController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        //
        // GET: /Competition/
        public ActionResult Index()
        {
            CompetitionViewModel viewModel = new CompetitionViewModel();
            viewModel.Competitions = _competitionService.GetCompetitions().Competitions;
            return View("Index", viewModel);
        }
    }
}
