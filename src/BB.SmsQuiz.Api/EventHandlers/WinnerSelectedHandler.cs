using BB.SmsQuiz.Infrastructure.Domain.Events;
using BB.SmsQuiz.Model.Competitions.Events;

namespace BB.SmsQuiz.Api.EventHandlers
{
    public class WinnerSelectedHandler : IDomainEventHandler<WinnerSelectedEvent>
    {
        public void Handle(WinnerSelectedEvent domainEvent)
        {
            // send a message here.
        }
    }
}