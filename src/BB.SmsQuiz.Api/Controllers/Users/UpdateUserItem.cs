using System;
using System.Runtime.Serialization;

namespace BB.SmsQuiz.Api.Controllers.Users
{
    [Serializable, DataContract]
    public class UpdateUserItem
    {
        [DataMember]
        public string Username { get; set; }
    }
}