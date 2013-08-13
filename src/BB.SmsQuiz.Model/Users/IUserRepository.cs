// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Users
{
    /// <summary>
    /// The UserRepository interface.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// The find by username.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        User FindByUsername(string username);
    }
}