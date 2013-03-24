using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Infrastructure.Domain.Events
{
    public interface IEventContainer
    {
        IEnumerable<IDomainEventHandler<T>> Handlers<T>(T domainEvent)
                                                        where T : IDomainEvent;
    }
}
