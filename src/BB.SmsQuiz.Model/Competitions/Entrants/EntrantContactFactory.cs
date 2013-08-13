// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntrantContactFactory.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BB.SmsQuiz.Model.Competitions.Entrants
{
    /// <summary>
    /// The entrant contact factory.
    /// </summary>
    public sealed class EntrantContactFactory
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="EntrantContactFactory"/> class from being created.
        /// </summary>
        private EntrantContactFactory()
        {
        }

        /// <summary>
        /// The get instance.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="IEntrantContact"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// Not implemented contact type.
        /// </exception>
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