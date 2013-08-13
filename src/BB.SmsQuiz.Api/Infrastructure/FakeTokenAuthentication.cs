// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeTokenAuthentication.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using BB.SmsQuiz.Infrastructure.Authentication;

namespace BB.SmsQuiz.Api.Infrastructure
{
    /// <summary>
    /// The fake token authentication.
    /// </summary>
    public class FakeTokenAuthentication : ITokenAuthentication
    {
        /// <summary>
        /// Validates the provided token.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsValid(string token)
        {
            return token == "teddybear";
        }

        /// <summary>
        /// Gets a new token.
        /// </summary>
        public string Token
        {
            get { return "teddybear"; }
        }
    }
}