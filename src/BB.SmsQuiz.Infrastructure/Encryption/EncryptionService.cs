using System.Security.Cryptography;

namespace BB.SmsQuiz.Infrastructure.Encryption
{
    /// <summary>
    /// 
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        /// <summary>
        /// Encrypts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public byte[] Encrypt(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var sha = new SHA1CryptoServiceProvider();
                return sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(value));
            }

            return new byte[]{};
        }
    }
}
