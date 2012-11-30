using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.ReadModel.Competition;
using BB.SmsQuiz.Services.Messaging.Competition;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Services.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class CompetitionService : ICompetitionService
    {
        /// <summary>
        /// The repository instance.
        /// </summary>
        private readonly ICompetitionRepository _repository = null;

        /// <summary>
        /// The mapper instance.
        /// </summary>
        private readonly IMapper _mapper = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionService" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public CompetitionService(ICompetitionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the competitions.
        /// </summary>
        /// <returns></returns>
        public GetCompetitionsResponse GetCompetitions()
        {
            GetCompetitionsResponse response = new GetCompetitionsResponse();
            IEnumerable<Competition> competitions = _repository.GetCompetitiions();
            response.Competitions = _mapper.Map<IEnumerable<Competition>, IEnumerable<CompetitionItem>>(competitions);
            return response;
        }
    }
}
