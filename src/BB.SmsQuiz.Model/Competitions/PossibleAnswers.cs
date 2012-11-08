using System.Linq;
using System.Collections.Generic;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Competitions
{
    public class PossibleAnswers : IValidatable
    {
        private const int ValidNumberOfAnswers = 4;

        private List<PossibleAnswer> _possibleAnswers = null;

        public IEnumerable<PossibleAnswer> Answers
        {
            get
            {
                return _possibleAnswers;
            }
        }

        public PossibleAnswer CorrectAnswer
        {
            get
            {
                return Answers.SingleOrDefault(a => a.IsCorrectAnswer);
            }
        }

        public void Add(PossibleAnswer possibleAnswer)
        {
            if (_possibleAnswers.Exists(p => p.Answer == possibleAnswer.Answer))
                throw new DuplicateAnswerException();

            _possibleAnswers.Add(possibleAnswer);
        }

        public PossibleAnswers()
        {
            _possibleAnswers = new List<PossibleAnswer>();
        }

        public bool IsValid
        {
            get 
            {
                return Answers.Count() == ValidNumberOfAnswers && CorrectAnswer != null;
            }
        }
    }
}
