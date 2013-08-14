// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserItemsConverter.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Users;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Mapping
{
    /// <summary>
    /// The user items converter.
    /// </summary>
    public class UserItemsConverter : ITypeConverter<IEnumerable<User>, IEnumerable<GetUser>>
    {
        /// <summary>
        /// Converts list of domain entity objects to api entity.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<GetUser> Convert(ResolutionContext context)
        {
            var from = (IEnumerable<User>)context.SourceValue;
            var list = new List<GetUser>();

            foreach (var user in from)
            {
                list.Add(Mapper.Map<User, GetUser>(user));
            }

            return list;
        }
    }
}