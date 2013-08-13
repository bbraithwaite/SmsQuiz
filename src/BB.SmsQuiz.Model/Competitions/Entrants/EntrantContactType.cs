// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntrantContactType.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BB.SmsQuiz.Model.Competitions.Entrants
{
    /// <summary>
    /// The contact types for entrants
    /// </summary>
    public enum EntrantContactType : int
    {
        /// <summary>
        /// Default value.
        /// </summary>
        NotSet = 0, 

        /// <summary>
        /// An SMS.
        /// </summary>
        Sms = 1, 

        /// <summary>
        /// An Email Address.
        /// </summary>
        Email = 2
    }
}