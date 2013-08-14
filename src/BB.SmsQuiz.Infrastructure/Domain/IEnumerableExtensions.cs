// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEnumerableExtensions.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// <summary>
//   Extension methods for IEnumarable
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BB.SmsQuiz.Infrastructure.Domain
{
    /// <summary>
    /// Extension methods for IEnumarable
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Selects the random.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T SelectRandom<T>(this IEnumerable<T> source)
        {
            return source.ElementAt(GetRandomArrayIndex(source.Count()));
        }

        /// <summary>
        /// Gets the random index of the array.
        /// </summary>
        /// <param name="arrayCount">
        /// The array count.
        /// </param>
        /// <returns>
        /// A random array index position.
        /// </returns>
        private static int GetRandomArrayIndex(int arrayCount)
        {
            return new Random().Next(arrayCount);
        }
    }
}