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
using BB.SmsQuiz.Infrastructure.UnitOfWork;
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
        /// The _unit of work.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The _competition repository.
        /// </summary>
        private readonly ICompetitionRepository _competitionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloseCompetitionController"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <param name="competitionRepository">
        /// The competition repository.
        /// </param>
        public CloseCompetitionController(
            IUnitOfWork unitOfWork, 
            ICompetitionRepository competitionRepository)
        {
            _competitionRepository = competitionRepository;
            _unitOfWork = unitOfWork;
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
            var competition = _competitionRepository.GetByCompetitionKey(id);

            if (competition == null)
            {
                throw new NotFoundException();
            }

            try
            {
                competition.PickWinner();
                _competitionRepository.Update(competition);
                _unitOfWork.SaveChanges();
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