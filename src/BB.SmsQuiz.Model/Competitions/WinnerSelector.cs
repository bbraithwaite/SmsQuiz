using System;
using System.Linq;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions.Events;
using System.Net.Mail;

namespace BB.SmsQuiz.Model.Competitions
{
    /// <summary>
    /// A class that contains the logic for selecting a competition winner.
    /// </summary>
    public class WinnerSelector
    {
        /// <summary>
        /// Gets or sets the competition statistics.
        /// </summary>
        /// <value>
        /// The competition statistics.
        /// </value>
        public CompetitionStatistics CompetitionStatistics { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WinnerSelector" /> class.
        /// </summary>
        /// <param name="competitionStatistics">The competition statistics.</param>
        public WinnerSelector(CompetitionStatistics competitionStatistics)
        {
            CompetitionStatistics = competitionStatistics;
        }

        /// <summary>
        /// Selects the winner.
        /// </summary>
        /// <remarks>
        /// Raises a WinnerSelectedEvent passing the details of the competition and its winner.
        /// </remarks>
        public void PickWinner()
        {
            if (CompetitionStatistics.HasCorrectAnswers)
            {
                CompetitionStatistics.Competition.Winner = GetCompetitionWinner();
                DomainEvents.Raise(new WinnerSelectedEvent(CompetitionStatistics.Competition));
            }
        }

        /// <summary>
        /// Gets the competition winner.
        /// </summary>
        /// <returns>A the winner of the competition, selected at random.</returns>
        private CompetitionWinner GetCompetitionWinner()
        {
            CompetitionWinner winner = new CompetitionWinner();
            winner.Entrant = CompetitionStatistics.CorrectAnswers.ElementAt(GetRandomEntrantIndex());
            return winner;
        }

        /// <summary>
        /// Gets the random index of the entrant.
        /// </summary>
        /// <returns>A random array index position.</returns>
        private int GetRandomEntrantIndex()
        {
            return new Random().Next(CompetitionStatistics.CorrectAnswers.Count());
        }
    }
}