using System.Collections.Generic;
using BB.SmsQuiz.ReadModel.Competition;

namespace BB.SmsQuiz.Services.Messaging.Competition
{
    /// <summary>
    ///  The response message for the get competitions service method.
    /// </summary>
    public class GetCompetitionsResponse
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
