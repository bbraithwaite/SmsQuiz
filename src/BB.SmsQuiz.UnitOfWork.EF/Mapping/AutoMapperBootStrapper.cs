// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperBootStrapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using AutoMapper;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.UnitOfWork.EF.Mapping
{
    /// <summary>
    /// The auto mapper boot strapper.
    /// </summary>
    public class AutoMapperBootStrapper
    {
        /// <summary>
        /// The configure.
        /// </summary>
        public static void Configure()
        {
            Mapper.CreateMap<Model.Users.User, EF.User>().ConvertUsing(new UserToUsers());
            Mapper.CreateMap<EF.User, Model.Users.User>().ConvertUsing(new UsersToUser());
        }
    }
}