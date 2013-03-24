using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.Infrastructure.Authentication;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;

namespace BB.SmsQuiz.Api.Controllers.Competitions
{
    /// <summary>
    /// The api calls for the competition details
    /// </summary>
    [UnhandledException, TokenValidationAttribute]
    public class CompetitionsController : BaseController
    {
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IMapper _mapper;
        private readonly ITokenAuthentication _authentication;

        public CompetitionsController(ICompetitionRepository competitionRepository, IMapper mapper,  ITokenAuthentication authentication)
        {
            _competitionRepository = competitionRepository;
            _mapper = mapper;
            _authentication = authentication;
        }

        // GET competition
        public IEnumerable<CompetitionItem> Get()
        {
            var competitions = _competitionRepository.Find("Status=@Status", new { Status = CompetitionStatus.Open });
            return _mapper.Map<IEnumerable<Competition>, IEnumerable<CompetitionItem>>(competitions);
        }

        // GET competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public CompetitionItem Get(Guid id)
        {
            var competition = _competitionRepository.FindByID(id);

            if (competition != null)
            {
                return _mapper.Map<Competition, CompetitionItem>(competition);
            }

            throw new NotFoundException();
        }

        // POST competition
        public HttpResponseMessage Post(CreateCompetitionItem item)
        {
            Competition competition = _mapper.Map<CreateCompetitionItem, Competition>(item);
            if (competition.IsValid)
            {
                _competitionRepository.Add(competition);

                CompetitionItem createdItem = _mapper.Map<Competition, CompetitionItem>(competition);
                return CreatedHttpResponse(createdItem.ID, createdItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, competition.ValidationErrors);
        }

        // PUT competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public HttpResponseMessage Put(int id, CreateCompetitionItem item)
        {
            Competition competition = _mapper.Map<CreateCompetitionItem, Competition>(item);

            if (competition.IsValid)
            {
                _competitionRepository.Update(competition);
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, competition.ValidationErrors);
        }

        // DELETE competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public HttpResponseMessage Delete(Guid id)
        {
            var competition = _competitionRepository.FindByID(id);

            if (competition != null)
            {
                _competitionRepository.Remove(competition);
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            throw new NotFoundException();
        }
    }
}