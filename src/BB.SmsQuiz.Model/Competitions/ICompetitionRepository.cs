using BB.SmsQuiz.Infrastructure.Domain;
using System.Collections.Generic;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// The competition repository.
    /// </summary>
    public interface ICompetitionRepository
    {
        IEnumerable<Competition> GetCompetitiions();
    }
}
