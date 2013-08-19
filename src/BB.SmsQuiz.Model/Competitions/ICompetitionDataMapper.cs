// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICompetitionDataMapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions.Entrants;
using BB.SmsQuiz.Model.Competitions.States;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// The CompetitionDataMapper interface.
    /// </summary>
    public interface ICompetitionDataMapper : IDataMapper<Competition>
    {
        /// <summary>
        /// Finds competitions by status i.e. open/closed.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<Competition> FindByStatus(CompetitionStatus status);

        /// <summary>
        /// Find by competition key.
        /// </summary>
        /// <param name="competitionKey">
        /// The competition Key.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        Competition FindByCompetitionKey(string competitionKey);
    }
}