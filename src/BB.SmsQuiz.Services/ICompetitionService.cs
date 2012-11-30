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
        /// <returns></returns>
        GetCompetitionsResponse GetCompetitions();
    }
}