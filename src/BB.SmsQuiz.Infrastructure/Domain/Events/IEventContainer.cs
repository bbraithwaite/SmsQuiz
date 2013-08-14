// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEventContainer.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   The EventContainer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace BB.SmsQuiz.Infrastructure.Domain.Events
{
    /// <summary>
    /// The EventContainer interface.
    /// </summary>
    public interface IEventContainer
    {
        /// <summary>
        /// The handlers.
        /// </summary>
        /// <param name="domainEvent">
        /// The domain event.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<IDomainEventHandler<T>> Handlers<T>(T domainEvent)
            where T : IDomainEvent;
    }
}