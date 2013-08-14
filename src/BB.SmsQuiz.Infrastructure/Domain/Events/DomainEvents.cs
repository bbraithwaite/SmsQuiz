// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainEvents.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   The domain events container for registering domain event callbacks.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
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
        /// The _actions.
        /// </summary>
        /// <remarks>Marked as ThreadStatic that each thread has its own callbacks</remarks>
        [ThreadStatic] private static List<Delegate> _actions;

        /// <summary>
        /// The container
        /// </summary>
        public static IEventContainer Container;

        /// <summary>
        /// Registers the specified callback for the given domain event.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="callback">
        /// The callback.
        /// </param>
        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (_actions == null)
                _actions = new List<Delegate>();

            _actions.Add(callback);
        }

        /// <summary>
        /// Raises the specified domain event and calls the event handlers.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="domainEvent">
        /// The domain event.
        /// </param>
        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            if (Container != null)
                foreach (var handler in Container.Handlers(domainEvent))
                    handler.Handle(domainEvent);

            // registered actions, typically used for unit tests.
            if (_actions != null)
                foreach (var action in _actions)
                    if (action is Action<T>)
                        ((Action<T>) action)(domainEvent);
        }
    }
}