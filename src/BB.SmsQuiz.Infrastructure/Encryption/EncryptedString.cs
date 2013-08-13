
namespace BB.SmsQuiz.Infrastructure.Encryption
{
    /// <summary>
    /// Encrypted string value object
    /// </summary>
    public class EncryptedString
    {
        /// <summary>
        /// The encrypted value
        /// </summary>
        public byte[] EncryptedValue { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptedString"/> class.
        /// </summary>
        /// <param name="encryptedValue">
        /// The encrypted value.
        /// </param>
        public EncryptedString(byte[] encryptedValue)
        {
            EncryptedValue = encryptedValue;
        }

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="encryptionService">The encryption service.</param>
        /// <returns></returns>
        public static EncryptedString Create(string value, IEncryptionService encryptionService)
        {
            return new EncryptedString(encryptionService.Encrypt(value));
        }
    }
}
