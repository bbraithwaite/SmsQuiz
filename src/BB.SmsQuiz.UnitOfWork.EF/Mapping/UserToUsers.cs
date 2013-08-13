// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserToUsers.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.UnitOfWork.EF.Mapping
{
    /// <summary>
    /// The user to users.
    /// </summary>
    public class UserToUsers : ITypeConverter<Model.Users.User, EF.User>
    {
        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Users"/>.
        /// </returns>
        public EF.User Convert(ResolutionContext context)
        {
            if (context.SourceValue == null) return null;

            var source = (Model.Users.User)context.SourceValue;

            var users = new EF.User()
                {
                    ID = source.ID, 
                    Username = source.Username, 
                    Password = source.Password.EncryptedValue
                };

            return users;
        }
    }
}