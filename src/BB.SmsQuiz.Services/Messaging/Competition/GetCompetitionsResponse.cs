using System.Collections.Generic;

namespace BB.SmsQuiz.Services.Messaging.Competition
{
    /// <summary>
    ///  The response message for the get competitions service method.
    /// </summary>
    public class GetCompetitionsResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the competitions.
        /// </summary>
        /// <value>
        /// The competitions.
        /// </value>
        public IEnumerable<CompetitionItem> Competitions { get; set; }
    }
}
