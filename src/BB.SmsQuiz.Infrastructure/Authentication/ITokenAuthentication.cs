// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITokenAuthentication.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   The TokenAuthentication interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.Infrastructure.Authentication
{
    /// <summary>
    /// The TokenAuthentication interface.
    /// </summary>
    public interface ITokenAuthentication
    {
        /// <summary>
        /// Gets the token.
        /// </summary>
        string Token { get; }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsValid(string token);
    }
}