using BB.SmsQuiz.Services.Messaging.Competition;

namespace BB.SmsQuiz.Services
{
    /// <summary>
    /// Competition service interface.
    /// </summary>
    public interface ICompetitionService
    {
        /// <summary>
        /// Gets the competitions.
        /// </summary>
        /// <returns>The response.</returns>
        GetCompetitionsResponse GetCompetitions(GetCompetitionsRequest request);

        /// <summary>
        /// Creates the competition.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        CreateCompetitionResponse CreateCompetition(CreateCompetitionRequest request);

        /// <summary>
        /// Gets the competition.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        GetCompetitionResponse GetCompetition(GetCompetitionRequest request);

        /// <summary>
        /// Deletes the competition.
        /// </summary>
        /// <param name="deleteCompetitionRequest">The delete competition request.</param>
        /// <returns>The response.</returns>
        DeleteCompetitionResponse DeleteCompetition(DeleteCompetitionRequest request);

        /// <summary>
        /// Enters the competition.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        EnterCompetitionResponse EnterCompetition(EnterCompetitionRequest request);
    }
}