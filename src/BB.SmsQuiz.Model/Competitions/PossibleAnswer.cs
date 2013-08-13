// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PossibleAnswer.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// A possible answer to a competition.
    /// </summary>
    public sealed class PossibleAnswer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PossibleAnswer"/> class.
        /// </summary>
        /// <param name="isCorrectAnswer">
        /// if set to <c>true</c> [is correct answer].
        /// </param>
        /// <param name="answerKey">
        /// The answer key.
        /// </param>
        /// <param name="answerText">
        /// The answer text.
        /// </param>
        public PossibleAnswer(bool isCorrectAnswer, CompetitionAnswer answerKey, string answerText)
        {
            IsCorrectAnswer = isCorrectAnswer;
            AnswerKey = answerKey;
            AnswerText = answerText;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PossibleAnswer"/> class.
        /// </summary>
        /// <param name="answerKey">
        /// The answer key.
        /// </param>
        public PossibleAnswer(CompetitionAnswer answerKey)
        {
            IsCorrectAnswer = false;
            AnswerKey = answerKey;
            AnswerText = string.Empty;
        }

        /// <summary>
        /// Gets a value indicating whether is correct answer.
        /// </summary>
        public bool IsCorrectAnswer { get; private set; }

        /// <summary>
        /// Gets the answer key.
        /// </summary>
        public CompetitionAnswer AnswerKey { get; private set; }

        /// <summary>
        /// Gets the answer text.
        /// </summary>
        public string AnswerText { get; private set; }
    }
}