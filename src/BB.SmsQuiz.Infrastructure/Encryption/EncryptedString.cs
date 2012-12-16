using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.SmsQuiz.Infrastructure.Encryption
{
    /// <summary>
    /// Encrypted string value object
    /// </summary>
    public class EncryptedString
    {
        public readonly string EncryptedValue;

        public EncryptedString(string encryptedString)
        {
            this.EncryptedValue = encryptedString;
        }

        public static EncryptedString Create(string value, IEncryptionService encryptionService)
        {
            return new EncryptedString(encryptionService.Encrypt(value));
        }
    }
}
