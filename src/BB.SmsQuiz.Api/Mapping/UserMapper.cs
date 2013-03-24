using System.Collections.Generic;
using AutoMapper;
using BB.SmsQuiz.Api.Controllers.Users;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Mapping
{
    public class UserItemsConverter : ITypeConverter<IEnumerable<User>, IEnumerable<UserItem>>
    {
        public IEnumerable<UserItem> Convert(ResolutionContext context)
        {
            var from = (IEnumerable<User>)context.SourceValue;
            var list = new List<UserItem>();

            foreach (var user in from)
            {
                list.Add(Mapper.Map<User, UserItem>(user));
            }

            return list;
        }
    }

    public class UserItemConverter : ITypeConverter<User, UserItem>
    {
        public UserItem Convert(ResolutionContext context)
        {
            var user = (User)context.SourceValue;
            var item = new UserItem()
            {
                ID = user.ID,
                Username = user.Username
            };

            return item;
        }
    }
}