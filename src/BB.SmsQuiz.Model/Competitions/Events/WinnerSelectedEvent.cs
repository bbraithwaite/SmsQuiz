using BB.SmsQuiz.Model.Entrants;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Competitions.Events
{
    public class WinnerSelectedEvent : IDomainEvent
    {
        public Competition Competition { get; set; }

        public WinnerSelectedEvent(Competition competition)
        {
            Competition = competition;
        }
    }
}