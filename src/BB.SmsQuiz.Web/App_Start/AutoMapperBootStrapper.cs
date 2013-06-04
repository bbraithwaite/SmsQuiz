using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BB.SmsQuiz.ApiModel.Users;
using BB.SmsQuiz.Web.Models;

namespace BB.SmsQuiz.Web.App_Start
{
    public class AutoMapperBootStrapper
    {
        public static void Configure()
        {
            Mapper.CreateMap<UserView, GetUser>();
            Mapper.CreateMap<GetUser, UserView>();
            Mapper.CreateMap<UserView, PostUser>();
            Mapper.CreateMap<UserView, PutUser>();
            //Mapper.CreateMap<IEnumerable<User>, IEnumerable<GetUser>>().ConvertUsing(new UserItemsConverter());
        }
    }
}