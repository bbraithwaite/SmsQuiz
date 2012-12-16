using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Services.Messaging.Competition;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions.States;
using System;
using System.Linq.Expressions;
using BB.SmsQuiz.Services.Messaging;
using BB.SmsQuiz.Model.Competitions.Entrants;

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
        public GetCompetitionsResponse GetCompetitions(GetCompetitionsRequest request)
        {
            GetCompetitionsResponse response = new GetCompetitionsResponse();
            IEnumerable<Competition> competitions = _repository.Find(GetQuery(request));
            response.Competitions = _mapper.Map<IEnumerable<Competition>, 
                                                IEnumerable<CompetitionItem>>(competitions);

            return response;
        }

        /// <summary>
        /// Creates the competition.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public CreateCompetitionResponse CreateCompetition(CreateCompetitionRequest request)
        {
            CreateCompetitionResponse response = new CreateCompetitionResponse();
            Competition competition = _mapper.Map<CompetitionItem, Competition>(request.Competition);

            if (competition.IsValid)
            {
                _repository.Add(competition);
                response.Status = ResponseStatus.OK;
            }
            else
            {
                response.Status = ResponseStatus.Invalid;
                response.ValidationErrors = _mapper.Map<IEnumerable<ValidationError>, 
                                                        IEnumerable<ValidationItem>>(competition.ValidationErrors.Items);
            }

            return response;
        }

        /// <summary>
        /// Gets the competition.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public GetCompetitionResponse GetCompetition(GetCompetitionRequest request)
        {
            GetCompetitionResponse response = new GetCompetitionResponse();
            Competition competition = _repository.FindByID(request.ID);
            response.Competition = _mapper.Map<Competition, CompetitionItem>(competition);
            return response;
        }

        /// <summary>
        /// Deletes the competition.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public DeleteCompetitionResponse DeleteCompetition(DeleteCompetitionRequest request)
        {
            DeleteCompetitionResponse response = new DeleteCompetitionResponse();
            _repository.Remove(new Competition() { ID = request.ID });
            return response;
        }

        /// <summary>
        /// Enters the competition.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The response.
        /// </returns>
        public EnterCompetitionResponse EnterCompetition(EnterCompetitionRequest request)
        {
            EnterCompetitionResponse response = new EnterCompetitionResponse();
            Entrant entrant = _mapper.Map<EntrantItem, Entrant>(request.Entrant);

            if (entrant.IsValid)
            {
                Competition competition = _repository.Find(c => c.CompetitionKey == request.Entrant.CompetitionKey).SingleOrDefault();

                if (competition != null)
                {
                    response.Status = ResponseStatus.OK;
                    competition.AddEntrant(entrant);
                    _repository.Update(competition);
                }
                else
                {
                    // this should live in the domain model!!!
                    response.Status = ResponseStatus.Invalid;
                    response.ValidationErrors = new List<ValidationItem>() { new ValidationItem("CompetitionKey", "Competition Key supplied is not valid.") { } };
                }
            }
            else
            {
                response.Status = ResponseStatus.Invalid;
                response.ValidationErrors = _mapper.Map<IEnumerable<ValidationError>,
                                                        IEnumerable<ValidationItem>>(entrant.ValidationErrors.Items);
            }
            
            return response;
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private Expression<Func<Competition, bool>> GetQuery(GetCompetitionsRequest request)
        {
            if (!string.IsNullOrEmpty(request.CompetitionKey))
            {
                return p => p.CompetitionKey == request.CompetitionKey;
            }
            else
            {
                // default to show active competitions
                return p => p.ClosingDate > DateTime.Now;
            }

            // Need to think about paging.
            //.Skip((request.PageIndex - 1) * PAGE_SIZE).Take(PAGE_SIZE);
        }
    }
}
