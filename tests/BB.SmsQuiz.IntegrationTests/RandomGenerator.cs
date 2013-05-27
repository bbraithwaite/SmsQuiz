using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.SmsQuiz.IntegrationTests
{
    internal class RandomGenerator
    {
        private static readonly string[] Chars = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        public static string GetRandomKey(int length)
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
