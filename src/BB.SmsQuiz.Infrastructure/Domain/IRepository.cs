// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   The repository interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.Infrastructure.Domain
{
    /// <summary>
    /// The repository interface.
    /// </summary>
    /// <typeparam name="T">
    /// The domain entity
    /// </typeparam>
    public interface IRepository<T> : IReadOnlyRepository<T> where T : EntityBase, IAggregateRoot
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        void Add(T item);

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        void Remove(T item);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        void Update(T item);

        /// <summary>
        /// Deletes the record by the ID
        /// </summary>
        /// <param name="id">
        /// The unique ID
        /// </param>
        void Delete(Guid id);
    }
}