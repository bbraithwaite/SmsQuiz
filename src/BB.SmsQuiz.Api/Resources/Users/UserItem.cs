using System;
using System.Runtime.Serialization;

namespace BB.SmsQuiz.Api.Resources.Users
{
    [Serializable, DataContract]
    public class UserItem
    {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string Username { get; set; }
    }
}
