// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersToUser.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Text;
using AutoMapper;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.UnitOfWork.EF.Mapping
{
    /// <summary>
    /// The users to user.
    /// </summary>
    public class UsersToUser : ITypeConverter<EF.User, Model.Users.User>
    {
        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public Model.Users.User Convert(ResolutionContext context)
        {
            if (context.SourceValue == null) return null;

            var source = (EF.User)context.SourceValue;

            var user = new Model.Users.User()
                {
                    ID = source.ID, 
                    Username = source.Username, 
                    Password = new EncryptedString(source.Password)
                };

            return user;
        }
    }
}