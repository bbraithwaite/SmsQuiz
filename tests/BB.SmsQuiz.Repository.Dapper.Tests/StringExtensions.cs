using System;

namespace BB.SmsQuiz.Repository.Tests
{
    internal static class StringExtensions
    {
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        internal static string GetRandomString(int size)
        {
            var rnd = new Random();
            var buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[rnd.Next(_chars.Length)];
            }
            return new string(buffer);
        }
    }
}
