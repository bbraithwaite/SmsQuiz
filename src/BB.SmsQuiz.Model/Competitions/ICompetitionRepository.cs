using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions.Entrants;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// The competition repository.
    /// </summary>
    public interface ICompetitionRepository : IRepository<Competition>
    {
    }
}