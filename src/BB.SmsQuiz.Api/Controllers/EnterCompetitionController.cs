// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnterCompetitionController.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.ApiModel.EnterCompetition;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Infrastructure.UnitOfWork;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;

namespace BB.SmsQuiz.Api.Controllers
{
    /// <summary>
    /// The enter competition controller.
    /// </summary>
    [UnhandledException]
    public class EnterCompetitionController : BaseController
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
        /// The _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterCompetitionController"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <param name="competitionRepository">
        /// The competition repository.
        /// </param>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        public EnterCompetitionController(
            IUnitOfWork unitOfWork, 
            ICompetitionRepository competitionRepository, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _competitionRepository = competitionRepository;
            _mapper = mapper;
        }

        // POST competitions/enter
        public HttpResponseMessage Post(PostEnterCompetition item)
        {
            var entrant = _mapper.Map<PostEnterCompetition, Entrant>(item);

            if (entrant.IsValid)
            {
                var competition = _competitionRepository.GetByCompetitionKey(item.CompetitionKey);
                competition.AddEntrant(entrant);
                _competitionRepository.Update(competition);
                _unitOfWork.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, item);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, entrant.ValidationErrors);
        }
    }
}