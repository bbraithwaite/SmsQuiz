using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BB.SmsQuiz.Infrastructure.Domain
{
    /// <summary>
    /// The repository interface.
    /// </summary>
    /// <typeparam name="T">The domain entity</typeparam>
    public interface IRepository<T> where T : EntityBase, IAggregateRoot
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Add(T item);

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Remove(T item);

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Update(T item);

        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T FindByID(Guid id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();
    }
}
