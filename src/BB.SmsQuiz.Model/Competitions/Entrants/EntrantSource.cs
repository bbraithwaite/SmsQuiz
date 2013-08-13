// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntrantSource.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BB.SmsQuiz.Model.Competitions.Entrants
{
    /// <summary>
    /// The sources from which entrants can register.
    /// </summary>
    public enum EntrantSource : int
    {
        /// <summary>
        /// Default value.
        /// </summary>
        NotSet = 0, 

        /// <summary>
        /// Via an SMS
        /// </summary>
        Sms = 1, 

        /// <summary>
        /// Via an Email
        /// </summary>
        Email = 2, 

        /// <summary>
        /// Via an App
        /// </summary>
        App = 3
    }
}