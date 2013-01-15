using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;

namespace BB.SmsQuiz.Api.Resources.EnterCompetition
{
    /// <summary>
    /// 
    /// </summary>
    [UnhandledException]
    public class EnterCompetitionController : BaseController
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
        /// Initializes a new instance of the <see cref="EnterCompetitionController" /> class.
        /// </summary>
        /// <param name="competitionRepository">The competition repository.</param>
        /// <param name="mapper">The mapper.</param>
        public EnterCompetitionController(ICompetitionRepository competitionRepository, IMapper mapper)
        {
            _competitionRepository = competitionRepository;
            _mapper = mapper;
        }

        // POST competitions/enter
        public HttpResponseMessage Post(CreateEntrantItem item)
        {
            Entrant entrant = _mapper.Map<CreateEntrantItem, Entrant>(item);

            if (entrant.IsValid)
            {
                Competition competition = _competitionRepository.Find(new { item.CompetitionKey }).Single();
                competition.AddEntrant(entrant);

                _competitionRepository.Update(competition);

                return Request.CreateResponse(HttpStatusCode.Created, item);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, entrant.ValidationErrors);
        }
    }
}