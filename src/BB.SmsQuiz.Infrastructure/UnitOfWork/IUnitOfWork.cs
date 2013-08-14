// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   The UnitOfWork interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.Infrastructure.UnitOfWork
{
    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        IContext Context { get; }

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int SaveChanges();
    }
}