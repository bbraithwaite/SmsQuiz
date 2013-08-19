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
        /// The _competition repository.
        /// </summary>
        private readonly ICompetitionDataMapper _competitionDataMapper;

        /// <summary>
        /// The _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnterCompetitionController"/> class.
        /// </summary>
        /// <param name="competitionDataMapper">
        /// The competition Data Mapper.
        /// </param>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        public EnterCompetitionController(
            ICompetitionDataMapper competitionDataMapper, 
            IMapper mapper)
        {
            _competitionDataMapper = competitionDataMapper;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new competition entrant.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        /// <remarks>
        /// POST competitions/enter
        /// </remarks>
        public HttpResponseMessage Post(PostEnterCompetition item)
        {
            var entrant = _mapper.Map<PostEnterCompetition, Entrant>(item);

            if (entrant.IsValid)
            {
                var competition = _competitionDataMapper.FindByCompetitionKey(item.CompetitionKey);
                competition.AddEntrant(entrant);
                _competitionDataMapper.Update(competition);

                return Request.CreateResponse(HttpStatusCode.Created, item);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, entrant.ValidationErrors);
        }
    }
}