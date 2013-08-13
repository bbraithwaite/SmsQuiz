// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserMapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Model.Users;
using BB.SmsQuiz.Repository.EF.Models;

namespace BB.SmsQuiz.Repository.EF
{
    /// <summary>
    /// The user mapper.
    /// </summary>
    internal static class UserMapper
    {
        /// <summary>
        /// The map from.
        /// </summary>
        /// <param name="userData">
        /// The user data.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        internal static IEnumerable<User> MapFrom(IEnumerable<UserData> userData)
        {
            return userData.Select(MapFrom);
        }

        /// <summary>
        /// The map from.
        /// </summary>
        /// <param name="userData">
        /// The user data.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        internal static User MapFrom(UserData userData)
        {
            if (userData == null)
            {
                return null;
            }

            var user = new User()
                {
                    ID = userData.ID, 
                    Username = userData.Username, 
                    Password = new EncryptedString(userData.Password)
                };

            return user;
        }

        /// <summary>
        /// The map to.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="UserData"/>.
        /// </returns>
        internal static UserData MapTo(User item)
        {
            if (item == null)
            {
                return null;
            }

            var data = new UserData
                {
                    ID = item.ID, 
                    Password = item.Password.EncryptedValue, 
                    Username = item.Username
                };

            return data;
        }
    }
}