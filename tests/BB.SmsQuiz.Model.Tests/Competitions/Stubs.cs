using System;
using System.Collections.Generic;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using BB.SmsQuiz.Model.Entrants;

namespace BB.SmsQuiz.Model.Tests.Competitions
{
    /// <summary>
    /// Mock instance methods for the competition tests.
    /// </summary>
    public class Stubs
    {
        /// <summary>
        /// Stubs the competition.
        /// </summary>
        /// <returns>
        /// A stub competition pre-programmed with expectated state.
        /// </returns>
        public static Competition StubCompetition()
        {
            return StubCompetition(true);
        }

        /// <summary>
        /// Gets the stub competition.
        /// </summary>
        /// <param name="includeCorrectAnswers">if set to <c>true</c> [include correct answers].</param>
        /// <returns>
        /// A stub competition pre-programmed with expectated state.
        /// </returns>
        public static Competition StubCompetition(bool includeCorrectAnswers)
        {
            Competition competition = new Competition(new PossibleAnswers(), new OpenState());
            competition.ClosingDate = new DateTime(2012, 11, 1);
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.A, Description = "Darth Vader", IsCorrectAnswer = true });
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.B, Description = "Obi Wan Kenobi" });
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.C, Description = "George Lucas" });
            competition.PossibleAnswers.Add(new PossibleAnswer() { Answer = CompetitionAnswer.D, Description = "Walt Disney" });
            competition.CompetitionKey = "WINPRIZE";
            competition.Entrants = StubEntrants(includeCorrectAnswers);

            return competition;
        }

        /// <summary>
        /// Gets the stub entrants.
        /// </summary>
        /// <param name="includeCorrectAnswers">if set to <c>true</c> [include correct answers].</param>
        /// <returns>
        /// A stub list of entrants pre-programmed with expectated state.
        /// </returns>
        private static IList<Entrant> StubEntrants(bool includeCorrectAnswers)
        {
            IList<Entrant> entrants = new List<Entrant>();

            if (includeCorrectAnswers)
            {
                // Correct answer
                for (int i = 0; i < 10; i++)
                {
                    entrants.Add(new Entrant() { Answer = CompetitionAnswer.A, EntryDate = new DateTime(2012, 10, 1) });
                }
            }

            // Incorrect answers - B
            for (int i = 0; i < 2; i++)
            {
                entrants.Add(new Entrant() { Answer = CompetitionAnswer.B, EntryDate = new DateTime(2012, 10, 1) });
            }

            // Incorrect answers - C
            for (int i = 0; i < 3; i++)
            {
                entrants.Add(new Entrant() { Answer = CompetitionAnswer.C, EntryDate = new DateTime(2012, 10, 2) });
            }

            // Incorrect answers - D
            for (int i = 0; i < 4; i++)
            {
                entrants.Add(new Entrant() { Answer = CompetitionAnswer.D, EntryDate = new DateTime(2012, 10, 1) });
            }

            // Add entrant which is past the entry date.
            entrants.Add(new Entrant() { Answer = CompetitionAnswer.D, EntryDate = new DateTime(2012, 12, 2) });

            return entrants;
        }
    }
}
