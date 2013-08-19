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
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Controllers
{
    /// <summary>
    /// The method calls for the competition details
    /// </summary>
    [UnhandledException, TokenValidationAttribute]
    public class CompetitionsController : BaseController
    {
        /// <summary>
        /// The _competition repository.
        /// </summary>
        private readonly ICompetitionDataMapper _competitionDataMapper;

        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly IUserDataMapper _userDataMapper;

        /// <summary>
        /// The _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionsController"/> class.
        /// </summary>
        /// <param name="competitionDataMapper">
        /// The competition Data Mapper.
        /// </param>
        /// <param name="userDataMapper">
        /// The user Data Mapper.
        /// </param>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        public CompetitionsController(
            ICompetitionDataMapper competitionDataMapper,
            IUserDataMapper userDataMapper,
            IMapper mapper)
        {
            _competitionDataMapper = competitionDataMapper;
            _userDataMapper = userDataMapper;
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
            var competitions = _competitionDataMapper.FindByStatus(CompetitionStatus.Open);
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
            var competition = _competitionDataMapper.FindById(id);

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
                CreatedBy = _userDataMapper.FindByAuthToken(RequestToken)
            };

            SetAnswers(item.Answers, item.CorrectAnswerKey, competition);

            if (competition.IsValid)
            {
                _competitionDataMapper.Insert(competition);

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
            var competition = _competitionDataMapper.FindById(id);

            if (competition == null)
            {
                throw new NotFoundException();
            }

            competition.ClosingDate = item.ClosingDate;
            competition.Question = item.Question;

            SetAnswers(item.Answers, item.CorrectAnswerKey, competition);

            if (competition.IsValid)
            {
                _competitionDataMapper.Update(competition);
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
            _competitionDataMapper.Delete(id);
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
            for (int i = 1; i <= answers.Count(); i++)
            {
                competition.PossibleAnswers.Add((CompetitionAnswer)i, answers[i - 1], i == correctAnswerKey);
            }
        }
    }
}