using System.Collections.Generic;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Users;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Mapping
{
    public class UserItemsConverter : ITypeConverter<IEnumerable<User>, IEnumerable<GetUser>>
    {
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

    public class UserItemConverter : ITypeConverter<User, GetUser>
    {
        public GetUser Convert(ResolutionContext context)
        {
            var user = (User)context.SourceValue;
            var item = new GetUser()
            {
                ID = user.ID,
                Username = user.Username
            };

            return item;
        }
    }
}