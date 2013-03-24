using System;
using System.Net;
using System.Net.Http;
using System.Web;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Model.Competitions;

namespace BB.SmsQuiz.Api.Controllers.CloseCompetitions
{
    public class CloseCompetitionController : BaseController
    {
        private readonly ICompetitionRepository _competitionRepository;

        public CloseCompetitionController(ICompetitionRepository competitionRepository, IMapper mapper)
        {
            _competitionRepository = competitionRepository;
        }

        // PUT competitions/B5608F8E-F449-E211-BB40-1040F3A7A3B1/enter
        public HttpResponseMessage Put(Guid id)
        {
            var competition = _competitionRepository.FindByID(id);

            if (competition == null) throw new NotFoundException();

            try
            {
                competition.PickWinner();
                _competitionRepository.Update(competition);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                throw new HttpUnhandledException();
            }
        }
    }
}