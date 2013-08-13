// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomGenerator.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.Api.IntegrationTests
{
    /// <summary>
    /// The random generator.
    /// </summary>
    internal class RandomGenerator
    {
        /// <summary>
        /// The chars.
        /// </summary>
        private static readonly string[] Chars = new[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};

        /// <summary>
        /// The get random string.
        /// </summary>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetRandomString(int length)
        {
            Random rnd = new Random();
            string result = string.Empty;

            for (int i = 0; i < length; i++)
            {
                result += Chars[rnd.Next(0, 9)];
            }

            return result;
        }
    }
}