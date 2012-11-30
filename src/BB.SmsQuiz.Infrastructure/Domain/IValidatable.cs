using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Infrastructure.Domain
{
    /// <summary>
    /// IValidatable interface.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool IsValid { get; }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>
        /// The validation errors.
        /// </value>
        ValidationErrors ValidationErrors { get; }
    }
}
