// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFormsAuthentication.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   The FormsAuthentication interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BB.SmsQuiz.Infrastructure.Authentication
{
    /// <summary>
    /// The FormsAuthentication interface.
    /// </summary>
    public interface IFormsAuthentication
    {
        /// <summary>
        /// The set authentication token.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        void SetAuthenticationToken(string token);

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        string AuthenticationToken { get; }

        /// <summary>
        /// The sign out.
        /// </summary>
        void SignOut();
    }
}