// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserDataMapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Users
{
    /// <summary>
    /// The UserDataMapper interface.
    /// </summary>
    public interface IUserDataMapper : IDataMapper<User>
    {
        /// <summary>
        /// Find user by username.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        User FindByUsername(string username);

        /// <summary>
        /// The find by authentication token.
        /// </summary>
        /// <param name="authenticationToken">
        /// The request token.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        User FindByAuthToken(string authenticationToken);
    }
}