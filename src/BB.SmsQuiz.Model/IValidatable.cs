using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Model
{
    /// <summary>
    /// An inteface for all entities that validate their state.
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
    }
}
