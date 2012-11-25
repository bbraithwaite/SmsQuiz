using System;
using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions.States;
using BB.SmsQuiz.Model.Entrants;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// A competition.
    /// </summary>
    public sealed class Competition : EntityBase
    {
        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>
        /// The question.
        /// </value>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the correct answer.
        /// </summary>
        /// <value>
        /// The correct answer.
        /// </value>
        public IPossibleAnswers PossibleAnswers { get; set; }

        /// <summary>
        /// Gets or sets the competition key.
        /// </summary>
        /// <value>
        /// The competition key.
        /// </value>
        public string CompetitionKey { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public User CreatedBy { get; set; }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Gets or sets the closing date.
        /// </summary>
        /// <value>
        /// The closing date.
        /// </value>
        public DateTime ClosingDate { get; set; }

        /// <summary>
        /// Gets the winner.
        /// </summary>
        /// <value>
        /// The winner.
        /// </value>
        public Entrant Winner { get; internal set; }

        /// <summary>
        /// Gets the competition state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public ICompetitionState State { get; private set; }

        /// <summary>
        /// Gets or sets the entrants.
        /// </summary>
        /// <value>
        /// The entrants.
        /// </value>
        public IEnumerable<Entrant> Entrants { get; set; }

        /// <summary>
        /// Gets the valid entrants.
        /// </summary>
        /// <value>
        /// The valid entrants.
        /// </value>
        public IEnumerable<Entrant> ValidEntrants
        {
            get
            {
                return Entrants.Where(e => e.EntryDate < ClosingDate);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [closing date has passed].
        /// </summary>
        /// <value>
        /// <c>true</c> if [closing date has passed]; otherwise, <c>false</c>.
        /// </value>
        public bool ClosingDateHasPassed
        {
            get
            {
                return (ClosingDate < DateTime.Now);
            }
        }

        /// <summary>
        /// Gets the correct entrants.
        /// </summary>
        /// <value>
        /// The correct entrants.
        /// </value>
        public IEnumerable<Entrant> CorrectEntrants
        {
            get
            {
                return ValidEntrants.Where(e => e.Answer == PossibleAnswers.CorrectAnswer.Answer);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has correct answers.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has correct answers; otherwise, <c>false</c>.
        /// </value>
        public bool HasCorrectEntrants
        {
            get
            {
                return this.CorrectEntrants.Count() > 0;
            }
        }

        /// <summary>
        /// Gets the incorrect answer count.
        /// </summary>
        /// <value>
        /// The incorrect answer count.
        /// </value>
        public IEnumerable<Entrant> IncorrectEntrants
        {
            get
            {
                return ValidEntrants.Where(e => e.Answer != PossibleAnswers.CorrectAnswer.Answer);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Competition" /> class.
        /// </summary>
        /// <param name="possibleAnswers">The possible answers.</param>
        /// <param name="state">The state.</param>
        /// <param name="statistics">The statistics.</param>
        public Competition(IPossibleAnswers possibleAnswers, ICompetitionState state)
        {
            CreationDate = DateTime.Now;
            PossibleAnswers = possibleAnswers;
            State = state;
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Question))
                ValidationErrors.Add("Question");

            if (string.IsNullOrEmpty(CompetitionKey))
                ValidationErrors.Add("CompetitionKey");
           
            if (ClosingDate == DateTime.MinValue)
                ValidationErrors.Add("ClosingDate");

            if (!PossibleAnswers.IsValid)
                ValidationErrors.AddRange(PossibleAnswers.ValidationErrors.Items);
        }

        /// <summary>
        /// Sets the state of the competition.
        /// </summary>
        /// <param name="state">The state.</param>
        public void SetCompetitionState(ICompetitionState state)
        {
            this.State = state;
        }

        /// <summary>
        /// Picks the winner.
        /// </summary>
        public void PickWinner()
        {
            this.State.PickWinner(this);
        }

        /// <summary>
        /// Gets the percentage of entrans.
        /// </summary>
        /// <param name="answer">The possible answer.</param>
        /// <returns>The percentage of entrants for a given answer.</returns>
        public decimal GetPercentageOfEntrants(CompetitionAnswer answer)
        {
            return (decimal)ValidEntrants.Count(e => e.Answer == answer) / ValidEntrants.Count() * 100;
        }
    }
}
