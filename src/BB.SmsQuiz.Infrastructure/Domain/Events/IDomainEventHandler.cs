// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDomainEventHandler.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   Domain event handler interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BB.SmsQuiz.Infrastructure.Domain.Events
{
    /// <summary>
    /// Domain event handler interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        /// <summary>
        /// Handles the specified domain event.
        /// </summary>
        /// <param name="domainEvent">
        /// The domain event.
        /// </param>
        void Handle(T domainEvent);
    }
}