using System.Linq;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;

namespace BB.SmsQuiz.Api.Controllers.EnterCompetition
{
    [UnhandledException]
    public class EnterCompetitionController : BaseController
    {
        private readonly ICompetitionRepository _competitionRepository;

        private readonly IMapper _mapper;

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