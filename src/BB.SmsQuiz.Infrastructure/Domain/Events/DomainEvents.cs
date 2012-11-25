using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BB.SmsQuiz.Infrastructure.Domain.Events
{
    /// <summary>
    /// The domain events container for registering domain event callbacks.
    /// </summary>
    /// <remarks>This implementation is based on the code example from this blog:
    /// http://www.udidahan.com/2009/06/14/domain-events-salvation/
    /// </remarks>
    public static class DomainEvents
    {
        /// <summary>
        /// The actions.
        /// </summary>
        /// <remarks>Marked as ThreadStatic that each thread has its own callbacks</remarks>
        [ThreadStatic]
        private static List<Delegate> actions;

        /// <summary>
        /// Registers the specified callback for the given domain event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback">The callback.</param>
        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        /// <summary>
        /// Clears the callbacks passed to Register on the current thread.
        /// </summary>
        public static void ClearCallbacks()
        {
            actions = null;
        }

        /// <summary>
        /// Raises the specified domain event and calls the event handlers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domainEvent">The domain event.</param>
        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            // TODO: wire in IoC container for service layer calls
            //if (Container != null)
                //foreach (var handler in Container.ResolveAll<IDomainEventHandlerFactory<T>>())
                   // handler.Handle(domainEvent);

            if (actions != null)
                foreach (var action in actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(domainEvent);
        }
    }
}