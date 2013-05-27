using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Model.Competitions.Entrants
{
    public sealed class EntrantContactFactory
    {
        private EntrantContactFactory()
        {
        }

        public static IEntrantContact GetInstance(EntrantContactType type)
        {
            switch (type)
            {
                case EntrantContactType.Sms:
                    return new SmsContact();
                case EntrantContactType.Email:
                    return new EmailContact();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
