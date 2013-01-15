using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using System.Linq;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Resources.Competitions
{
    /// <summary>
    /// The api calls for the competition details
    /// </summary>
    [UnhandledException]
    public class CompetitionsController : BaseController
    {
        /// <summary>
        /// The _competition repository
        /// </summary>
        private readonly ICompetitionRepository _competitionRepository;

        /// <summary>
        /// The mapper instance.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionsController" /> class.
        /// </summary>
        /// <param name="competitionRepository">The competition repository.</param>
        /// <param name="mapper">The mapper.</param>
        public CompetitionsController(ICompetitionRepository competitionRepository, IMapper mapper)
        {
            _competitionRepository = competitionRepository;
            _mapper = mapper;
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

            if (competition == null)
            {
                throw new NotFoundException();
            }

            _competitionRepository.Remove(competition);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}