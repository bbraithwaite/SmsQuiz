using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Infrastructure.Encryption
{
    public interface IEncryptionService
    {
        string Encrypt(string value);
    }
}
