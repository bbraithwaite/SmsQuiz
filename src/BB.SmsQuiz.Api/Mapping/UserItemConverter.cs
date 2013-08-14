// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserItemConverter.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Users;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Mapping
{
    /// <summary>
    /// The user item converter.
    /// </summary>
    public class UserItemConverter : ITypeConverter<User, GetUser>
    {
        /// <summary>
        /// Converts item from domain entity to api model.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="GetUser"/>.
        /// </returns>
        public GetUser Convert(ResolutionContext context)
        {
            var user = (User) context.SourceValue;
            var item = new GetUser()
                {
                    ID = user.ID, 
                    Username = user.Username
                };

            return item;
        }
    }
}