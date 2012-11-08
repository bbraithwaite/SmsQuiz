using System;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Users
{
    public class User : IValidatable
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid
        {
            get 
            {
                return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
            }
        }
    }
}
