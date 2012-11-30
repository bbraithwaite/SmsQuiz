using System.Collections.Generic;
using System.Web.Http;
using BB.SmsQuiz.ReadModel.Competition;
using BB.SmsQuiz.Services;

namespace BB.SmsQuiz.Api.Controllers
{
    /// <summary>
    /// The api calls for the competition details
    /// </summary>
    public class CompetitionController : ApiController
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

        // GET api/<controller>
        public IEnumerable<CompetitionItem> Get()
        {
            return _competitionService.GetCompetitions().Competitions;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}