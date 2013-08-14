// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionsController.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Infrastructure.UnitOfWork;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Controllers
{
    /// <summary>
    /// The api calls for the competition details
    /// </summary>
    [UnhandledException, TokenValidationAttribute]
    public class CompetitionsController : BaseController
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
        /// The user repository.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionsController"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <param name="competitionRepository">
        /// The competition repository.
        /// </param>
        /// <param name="userRepository">
        /// The user Repository.
        /// </param>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        public CompetitionsController(
            IUnitOfWork unitOfWork, 
            ICompetitionRepository competitionRepository, 
            IUserRepository userRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _competitionRepository = competitionRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all open competitions.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<GetCompetition> Get()
        {
            var competitions = _competitionRepository.GetByStatus(CompetitionStatus.Open);
            return _mapper.Map<IEnumerable<Competition>, IEnumerable<GetCompetition>>(competitions);
        }

        /// <summary>
        /// Get competition by ID.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="GetCompetition"/>.
        /// </returns>
        /// <exception cref="NotFoundException">
        /// Raised when id is invalid.
        /// </exception>
        /// <remarks>
        /// GET competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        /// </remarks>
        public GetCompetition Get(Guid id)
        {
            var competition = _competitionRepository.FindById(id);

            if (competition != null)
            {
                return _mapper.Map<Competition, GetCompetition>(competition);
            }

            throw new NotFoundException();
        }

        /// <summary>
        /// Create a new competition.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        /// <remarks>
        /// POST competition
        /// </remarks>
        public HttpResponseMessage Post(PostCompetition item)
        {
            var competition = new Competition()
            {
                Question = item.Question,
                ClosingDate = item.ClosingDate,
                CompetitionKey = item.CompetitionKey,
                // TODO: add get user by auth token method
                CreatedBy = _userRepository.GetAll().First()
            };

            SetAnswers(item.Answers, item.CorrectAnswerKey, competition);

            if (competition.IsValid)
            {
                _competitionRepository.Add(competition);
                _unitOfWork.SaveChanges();

                var createdItem = _mapper.Map<Competition, GetCompetition>(competition);
                return CreatedHttpResponse(createdItem.Id, createdItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, competition.ValidationErrors);
        }

        /// <summary>
        /// Update an existing competition.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        /// <exception cref="NotFoundException">
        /// Raised when id is invalid.
        /// </exception>
        /// <remarks>
        /// PUT competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        /// </remarks>
        public HttpResponseMessage Put(Guid id, PutCompetition item)
        {
            var competition = _competitionRepository.FindById(id);

            if (competition == null)
            {
                throw new NotFoundException();
            }

            competition.ClosingDate = item.ClosingDate;
            competition.Question = item.Question;

            SetAnswers(item.Answers, item.CorrectAnswerKey, competition);

            if (competition.IsValid)
            {
                _competitionRepository.Update(competition);
                _unitOfWork.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, competition.ValidationErrors);
        }

        /// <summary>
        /// Delete an existing competition.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        /// <remarks>
        /// DELETE competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        /// </remarks>
        public HttpResponseMessage Delete(Guid id)
        {
            _competitionRepository.Delete(id);
            _unitOfWork.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Sets the answers from the request object to the domain model.
        /// </summary>
        /// <param name="answers">
        /// The answers.
        /// </param>
        /// <param name="correctAnswerKey">
        /// The correct answer key.
        /// </param>
        /// <param name="competition">
        /// The competition.
        /// </param>
        private static void SetAnswers(string[] answers, int correctAnswerKey, Competition competition)
        {
            for (int i = 0; i < answers.Count(); i++)
            {
                var key = (CompetitionAnswer)Enum.Parse(typeof(CompetitionAnswer), answers[i]);
                competition.PossibleAnswers.Add(key, answers[i], (int)key == correctAnswerKey);
            }
        }
    }
}