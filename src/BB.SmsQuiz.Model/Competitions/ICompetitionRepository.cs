// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICompetitionRepository.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions.States;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// The CompetitionRepository interface.
    /// </summary>
    public interface ICompetitionRepository : IRepository<Competition>
    {
        /// <summary>
        /// The get by competition key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        Competition GetByCompetitionKey(string key);

        /// <summary>
        /// The get by status.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<Competition> GetByStatus(CompetitionStatus status);
    }
}