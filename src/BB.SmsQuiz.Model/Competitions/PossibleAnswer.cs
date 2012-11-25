
namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// A possible answer to a competition.
    /// </summary>
    public sealed class PossibleAnswer
    {
        public bool IsCorrectAnswer { get; set; }
        public CompetitionAnswer Answer { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PossibleAnswer" /> class.
        /// </summary>
        public PossibleAnswer() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PossibleAnswer" /> class.
        /// </summary>
        /// <param name="isCorrectAnswer">if set to <c>true</c> [is correct answer].</param>
        /// <param name="answer">The answer.</param>
        public PossibleAnswer(bool isCorrectAnswer, CompetitionAnswer answer)
        {
            IsCorrectAnswer = isCorrectAnswer;
            Answer = answer;
        }

        public string Description { get; set; }
    }
}
