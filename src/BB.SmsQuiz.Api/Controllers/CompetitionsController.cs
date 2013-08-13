// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionsController.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Infrastructure.UnitOfWork;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;

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
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        public CompetitionsController(
            IUnitOfWork unitOfWork, 
            ICompetitionRepository competitionRepository, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _competitionRepository = competitionRepository;
            _mapper = mapper;
        }

        // GET competition
        public IEnumerable<GetCompetition> Get()
        {
            var competitions = _competitionRepository.GetByStatus(CompetitionStatus.Open);
            return _mapper.Map<IEnumerable<Competition>, IEnumerable<GetCompetition>>(competitions);
        }

        // GET competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public GetCompetition Get(Guid id)
        {
            var competition = _competitionRepository.FindById(id);

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
                _unitOfWork.SaveChanges();

                var createdItem = _mapper.Map<Competition, GetCompetition>(competition);
                return CreatedHttpResponse(createdItem.Id, createdItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, competition.ValidationErrors);
        }

        // PUT competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public HttpResponseMessage Put(Guid id, PutCompetition item)
        {
            var competition = _competitionRepository.FindById(id);

            if (competition == null)
            {
                throw new NotFoundException();
            }

            var updatingCompetition = _mapper.Map<PutCompetition, Competition>(item);

            // white listed properties
            competition.ClosingDate = updatingCompetition.ClosingDate;
            competition.Question = updatingCompetition.Question;
            competition.PossibleAnswers = updatingCompetition.PossibleAnswers;

            if (competition.IsValid)
            {
                _competitionRepository.Update(competition);
                _unitOfWork.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, competition.ValidationErrors);
        }

        // DELETE competition/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public HttpResponseMessage Delete(Guid id)
        {
            _competitionRepository.Delete(id);
            _unitOfWork.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}