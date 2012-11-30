using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Infrastructure.Domain
{
    /// <summary>
    /// The repository interface.
    /// </summary>
    /// <typeparam name="T">The domain entity</typeparam>
    public interface IRepository<T> : IQueryable<T> where T : IAggregateRoot
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
        /// Saves the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Save(T item);
    }
}
