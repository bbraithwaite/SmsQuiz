// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationError.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   Validation error.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BB.SmsQuiz.Infrastructure.Domain
{
    /// <summary>
    /// Validation error.
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public ValidationError(string propertyName, string message)
        {
            this.PropertyName = propertyName;
            this.Message = message;
        }
    }
}