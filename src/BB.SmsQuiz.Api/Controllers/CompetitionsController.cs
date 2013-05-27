using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Infrastructure.Authentication;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace BB.SmsQuiz.Api.Controllers
{
    /// <summary>
    /// The api calls for the competition details
    /// </summary>
    [UnhandledException, TokenValidationAttribute]
    public class CompetitionsController : BaseController
    {
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IMapper _mapper;

        public CompetitionsController(ICompetitionRepository competitionRepository, IMapper mapper,  ITokenAuthentication authentication)
        {
            _competitionRepository = competitionRepository;
            _mapper = mapper;
        }

        // GET competition
        public IEnumerable<GetCompetition> Get()
        {
            var competitions = _competitionRepository.Find("Status=@Status", new { Status = CompetitionStatus.Open });
            return _mapper.Map<IEnumerable<Competition>, IEnumerable<GetCompetition>>(competitions);
        }

        // GET competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public GetCompetition Get(Guid id)
        {
            var competition = _competitionRepository.FindByID(id);

            if (competition != null)
            {
                return _mapper.Map<Competition, GetCompetition>(competition);
            }

            throw new NotFoundException();
        }

        // POST competition
        public HttpResponseMessage Post(PostCompetition item)
        {
            var competition = _mapper.Map<PostCompetition, Competition>(item);

            if (competition.IsValid)
            {
                _competitionRepository.Add(competition);
                var createdItem = _mapper.Map<Competition, GetCompetition>(competition);

                return CreatedHttpResponse(createdItem.Id, createdItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, competition.ValidationErrors);
        }

        // PUT competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public HttpResponseMessage Put(int id, PostCompetition item)
        {
            Competition competition = _mapper.Map<PostCompetition, Competition>(item);

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