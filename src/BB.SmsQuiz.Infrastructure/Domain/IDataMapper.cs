// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataMapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BB.SmsQuiz.Infrastructure.Domain
{
    /// <summary>
    /// The DataMapper interface.
    /// </summary>
    /// <typeparam name="T">
    /// A domain entity.
    /// </typeparam>
    public interface IDataMapper<T> where T : EntityBase
    {
        /// <summary>
        /// Finds an entity by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T FindById(Guid id);

        /// <summary>
        /// Finds all entities.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> FindAll();

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        void Insert(T item);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        void Update(T item);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        void Delete(Guid id);
    }
}