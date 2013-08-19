// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CloseCompetitionController.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Http;
using System.Web;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;

namespace BB.SmsQuiz.Api.Controllers
{
    /// <summary>
    /// The close competition controller.
    /// </summary>
    [UnhandledException, TokenValidationAttribute]
    public class CloseCompetitionController : BaseController
    {
        /// <summary>
        /// The _competition repository.
        /// </summary>
        private readonly ICompetitionDataMapper _competitionDataMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloseCompetitionController"/> class.
        /// </summary>
        /// <param name="competitionDataMapper">
        /// The competition Data Mapper.
        /// </param>
        public CloseCompetitionController(
            ICompetitionDataMapper competitionDataMapper)
        {
            _competitionDataMapper = competitionDataMapper;
        }

        /// <summary>
        /// Calls the close method on the competition.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        /// <remarks>
        /// PUT competitions/close/abcdef
        /// </remarks>
        public HttpResponseMessage Put(string id)
        {
            var competition = _competitionDataMapper.FindByCompetitionKey(id);

            if (competition == null)
            {
                throw new NotFoundException();
            }

            try
            {
                competition.PickWinner();
                _competitionDataMapper.Update(competition);
                return Request.CreateResponse(SetStatus(competition));
            }
            catch (Exception)
            {
                throw new HttpUnhandledException();
            }
        }

        /// <summary>
        /// Sets the status based on competition status change.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        /// <returns>
        /// The <see cref="HttpStatusCode"/>.
        /// </returns>
        /// <remarks>
        /// If a competition is not closed, the status will not be closed.
        /// </remarks>
        private HttpStatusCode SetStatus(Competition competition)
        {
            return competition.Status == CompetitionStatus.Open ? HttpStatusCode.NotModified : HttpStatusCode.OK;
        }
    }
}