using System.Collections;
using System.Collections.Generic;
using BB.SmsQuiz.Infrastructure.Domain.Events;
using Ninject;

namespace BB.SmsQuiz.Api.App_Start
{
    public class NinjectEventContainer : IEventContainer
    {
        private readonly IKernel _kernerl;

        public NinjectEventContainer(IKernel kernal)
        {
            _kernerl = kernal;
        }

        public IEnumerable<IDomainEventHandler<T>> Handlers<T>(T domainEvent) where T : IDomainEvent
        {
            return _kernerl.GetAll<IDomainEventHandler<T>>();
        }
    }
}