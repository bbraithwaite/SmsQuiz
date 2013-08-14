// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAggregateRoot.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   The aggregate root for use in the repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.Infrastructure.Domain
{
    /// <summary>
    /// The aggregate root for use in the repository.
    /// </summary>
    /// <remarks>
    /// This indicates what objects can be directly loaded from the repository.
    /// </remarks>
    public interface IAggregateRoot
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        Guid ID { get; set; }
    }
}