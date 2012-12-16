using System.Collections.Generic;
using System.Web.Http;
using BB.SmsQuiz.Services;
using System;
using BB.SmsQuiz.Services.Messaging.Competition;

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
        public IEnumerable<CompetitionItem> Get(int pageIndex, int status)
        {
            return _competitionService.GetCompetitions(new GetCompetitionsRequest() { PageIndex = pageIndex }).Competitions;
        }

        // GET api/<controller>/5
        public CompetitionItem Get(Guid id)
        {
            return _competitionService.GetCompetition(new GetCompetitionRequest() { ID = id }).Competition;
        }

        // POST api/<controller>
        public void Post([FromBody]CompetitionItem value)
        {
            var response = _competitionService.CreateCompetition(new CreateCompetitionRequest() { Competition = value });
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]CompetitionItem value)
        {
            var response = _competitionService.CreateCompetition(new CreateCompetitionRequest() { Competition = value });
        }

        // DELETE api/<controller>/5
        public void Delete(Guid id)
        {
            var response = _competitionService.DeleteCompetition(new DeleteCompetitionRequest() { ID = id });
        }
    }
}