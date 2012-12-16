using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;
using BB.SmsQuiz.Model.Competitions.States;
using BB.SmsQuiz.Model.Users;
using Dapper;
using RepoWrapper;

namespace BB.SmsQuiz.Repository.Dapper
{
    public sealed class CompetitionRepository : Repository<Competition>, ICompetitionRepository
    {
        private const string TableName = "Competitions";

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionRepository" /> class.
        /// </summary>
        public CompetitionRepository() : base(TableName) { }

        /// <summary>
        /// Mappings the query parameter values from the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A mapping object of sql parameters to values.</returns>
        internal override dynamic Mapping(Competition item)
        {
            return new
            {
                ClosingDate = item.ClosingDate,
                CompetitionKey = item.CompetitionKey,
                CreatedDate = item.CreatedDate,
                Question = item.Question,
                CreatedByID = item.CreatedBy.ID,
                WinnerID = (item.Winner != null) ? item.Winner.ID : Guid.Empty,
                Status = item.State.Status,
                ID = item.ID
            };
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public override void Add(Competition item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                item.ID = cn.Insert<Guid>(TableName, parameters);

                SavePossibleAnswers(cn, item);
                SaveEntrants(cn, item);
            }
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public override void Update(Competition item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                cn.Update(TableName, parameters);

                SavePossibleAnswers(cn, item, updating: true);
                SaveEntrants(cn, item);
            }
        }

        /// <summary>
        /// Finds the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override Competition FindByID(Guid id)
        {
            string sql = "SELECT C.*, U.*, E.*, C.Status As ID FROM Competitions C INNER JOIN Users U ON C.CreatedByID = U.ID LEFT JOIN Entrants E ON E.ID = C.WinnerID WHERE C.ID=@ID; SELECT * FROM PossibleAnswers WHERE CompetitionID=@ID";
            return GetCompetitionData(sql, new { ID = id }).SingleOrDefault();
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>A list of all competition.</returns>
        public override IEnumerable<Competition> FindAll()
        {
            string sql = "SELECT C.*, U.*, E.*, C.Status As ID FROM Competitions C INNER JOIN Users U ON C.CreatedByID = U.ID LEFT JOIN Entrants E ON E.ID = C.WinnerID; SELECT * FROM PossibleAnswers";
            return GetCompetitionData(sql);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public override void Remove(Competition item)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM Competitions WHERE ID=@ID; DELETE FROM PossibleAnswers WHERE CompetitionID=@ID", new { ID = item.ID });
            }
        }

        /// <summary>
        /// Maps the possible answers and links to competition.
        /// </summary>
        /// <param name="competition">The competition.</param>
        /// <param name="possibleAnswers">The possible answers.</param>
        private static void MapPossibleAnswers(Competition competition, IEnumerable<dynamic> possibleAnswers)
        {
            if (competition == null) return;

            foreach (var m in possibleAnswers.Where(p => p.CompetitionID == competition.ID))
            {
                competition.PossibleAnswers.Add(new PossibleAnswer(m.IsCorrectAnswer, (CompetitionAnswer)m.AnswerKey, m.AnswerText));
            }
        }

        /// <summary>
        /// Gets the competition data.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns>The competition data that matches the sql query.</returns>
        private List<Competition> GetCompetitionData(string sql, dynamic param = null)
        {
            List<Competition> competitions = null;
            IEnumerable<dynamic> possibleAnswers = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                using (var multi = cn.QueryMultiple(sql, (object)param))
                {
                    competitions = multi.Read<Competition, User, dynamic, byte, Competition>((comp, user, winner, status) =>
                    {
                        comp.CreatedBy = user;
                        comp.SetCompetitionState(CompetitionStateFactory.GetInstance((CompetitionStatus)status));
                        comp.Winner = MapEntrant(winner);
                        return comp;
                    }).ToList();

                    possibleAnswers = multi.Read().ToList();
                }
                cn.Close();
            }

            foreach (var comp in competitions)
            {
                MapPossibleAnswers(comp, possibleAnswers);
            }

            return competitions;
        }

        /// <summary>
        /// Maps the entrant.
        /// </summary>
        /// <param name="winner">The winner.</param>
        /// <returns>An entrant item.</returns>
        private Entrant MapEntrant(dynamic winner)
        {
            if (winner == null) return null;

            IEntrantContact contact = EntrantContactFactory.GetInstance((EntrantContactType)winner.ContactType);
            contact.Contact = winner.ContactDetail;

            Entrant entrant = new Entrant()
            {
                ID = winner.ID,
                Answer = (CompetitionAnswer)winner.AnswerKey,
                EntryDate = winner.EntryDate,
                Source = (EntrantSource)winner.Source,
                Contact = contact
            };

            return entrant;
        }

        /// <summary>
        /// Saves the possible answers.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="updating">if set to <c>true</c> [updating].</param>
        private void SavePossibleAnswers(IDbConnection cn, Competition item, bool updating = false)
        {
            string sql = "INSERT INTO PossibleAnswers VALUES (@CompetitionID, @AnswerKey, @AnswerText, @IsCorrectAnswer)";

            if (updating)
            {
                sql = "UPDATE PossibleAnswers SET AnswerText=@AnswerText, IsCorrectAnswer=@IsCorrectAnswer WHERE CompetitionID = @CompetitionID AND AnswerKey=@AnswerKey";
            }

            foreach (var answer in item.PossibleAnswers.Answers)
            {
                var param = new
                {
                    CompetitionID = item.ID,
                    AnswerKey = answer.AnswerKey,
                    AnswerText = answer.AnswerText,
                    IsCorrectAnswer = answer.IsCorrectAnswer
                };

                cn.Execute(sql, param);
            }
        }

        /// <summary>
        /// Saves the entrants.
        /// </summary>
        /// <param name="item">The item.</param>
        private void SaveEntrants(IDbConnection cn, Competition item)
        {
            foreach (Entrant entrant in item.Entrants.Where(e => e.ID == Guid.Empty))
            {
                var param = new
                {
                    AnswerKey = entrant.Answer,
                    EntryDate = entrant.EntryDate,
                    Source = entrant.Source,
                    ContactType = entrant.Contact.ContactType,
                    ContactDetail = entrant.Contact.Contact,
                    CompetitionID = item.ID
                };

                entrant.ID = cn.Insert<Guid>("Entrants", param);
            }
        }
    }
}
