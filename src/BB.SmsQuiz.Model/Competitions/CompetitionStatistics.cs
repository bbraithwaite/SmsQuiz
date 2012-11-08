using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Model.Entrants;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// Competition Statistics that report on competition data.
    /// </summary>
    public class CompetitionStatistics
    {
        /// <summary>
        /// Gets or sets the competition.
        /// </summary>
        /// <value>
        /// The competition.
        /// </value>
        public Competition Competition { get; private set; }

        /// <summary>
        /// Gets or sets the entrants.
        /// </summary>
        /// <value>
        /// The entrants.
        /// </value>
        private IEnumerable<Entrant> Entrants { get; set; }

        /// <summary>
        /// Gets the correct answer count.
        /// </summary>
        /// <value>
        /// The correct answer count.
        /// </value>
        public IEnumerable<Entrant> CorrectAnswers
        {
            get
            {
                return ValidEntrants.Where(e => e.Answer == Competition.CorrectAnswer);
            }
        }

        /// <summary>
        /// Gets the incorrect answer count.
        /// </summary>
        /// <value>
        /// The incorrect answer count.
        /// </value>
        public IEnumerable<Entrant> IncorrectAnswers
        {
            get
            {
                return ValidEntrants.Where(e => e.Answer != Competition.CorrectAnswer);
            }
        }

        /// <summary>
        /// Gets the valid entrants.
        /// </summary>
        /// <value>
        /// The valid entrants within the closing date of the competition.
        /// </value>
        public IEnumerable<Entrant> ValidEntrants
        {
            get
            {
                return Entrants.Where(e => e.EntryDate < Competition.ClosingDate);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionStatistics" /> class.
        /// </summary>
        /// <param name="competition">The competition.</param>
        /// <param name="entrants">The entrants.</param>
        public CompetitionStatistics(Competition competition, IEnumerable<Entrant> entrants)
        {
            Competition = competition;
            Entrants = entrants;
        }

        /// <summary>
        /// Gets the percentage of entrans.
        /// </summary>
        /// <param name="answer">The possible answer.</param>
        /// <returns>The percentage of entrants for a given answer.</returns>
        public decimal GetPercentageOfEntrans(CompetitionAnswer answer)
        {
            return (decimal)ValidEntrants.Count(e => e.Answer == answer) / ValidEntrants.Count() * 100;
        }
    }
}
