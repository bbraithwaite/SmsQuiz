// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationErrors.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   Validation errors.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace BB.SmsQuiz.Infrastructure.Domain
{
    /// <summary>
    /// Validation errors.
    /// </summary>
    public class ValidationErrors
    {
        /// <summary>
        /// The _errors
        /// </summary>
        private List<ValidationError> _errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrors" /> class.
        /// </summary>
        public ValidationErrors()
        {
            _errors = new List<ValidationError>();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IList<ValidationError> Items
        {
            get { return _errors; }
        }

        /// <summary>
        /// Adds the specified property name.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        public void Add(string propertyName)
        {
            _errors.Add(new ValidationError(propertyName, propertyName + " is required."));
        }

        /// <summary>
        /// Adds the specified property name.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        public void Add(string propertyName, string errorMessage)
        {
            _errors.Add(new ValidationError(propertyName, errorMessage));
        }

        /// <summary>
        /// Adds the specified error.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        public void Add(ValidationError error)
        {
            _errors.Add(error);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        public void AddRange(IList<ValidationError> errors)
        {
            _errors.AddRange(errors);
        }

        /// <summary>
        /// Clears the items.
        /// </summary>
        internal void Clear()
        {
            _errors.Clear();
        }
    }
}