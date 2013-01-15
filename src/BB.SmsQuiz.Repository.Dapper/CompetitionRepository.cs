using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;
using BB.SmsQuiz.Model.Competitions.States;
using BB.SmsQuiz.Model.Users;
using Dapper;

namespace BB.SmsQuiz.Repository.Dapper
{
    public sealed class CompetitionRepository : Repository<Competition>, ICompetitionRepository
    {
        /// <summary>
        /// The table name
        /// </summary>
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
            const string sql = "SELECT * FROM PossibleAnswers WHERE CompetitionID=@ID;SELECT * FROM Entrants WHERE CompetitionID = @ID;SELECT C.*, U.* FROM Competitions C INNER JOIN Users U ON C.CreatedByID = U.ID WHERE C.ID=@ID;";
            return GetCompetitionData(sql, new { ID = id }).SingleOrDefault();
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>A list of all competition.</returns>
        public override IEnumerable<Competition> FindAll()
        {
            const string sql = "SELECT * FROM PossibleAnswers;SELECT * FROM Entrants;SELECT C.*, U.* FROM Competitions C INNER JOIN Users U ON C.CreatedByID = U.ID;";
            return GetCompetitionData(sql);
        }

        /// <summary>
        /// Finds the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public override IEnumerable<Competition> Find(string query, dynamic param)
        {
            // TODO: optimise queries to apply where query
            string sql = "SELECT * FROM PossibleAnswers;SELECT * FROM Entrants;SELECT C.*, U.* FROM Competitions C INNER JOIN Users U ON C.CreatedByID = U.ID WHERE " + query + ";";
            return GetCompetitionData(sql, (object)param);
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
                cn.Execute("DELETE FROM PossibleAnswers WHERE CompetitionID=@ID;DELETE FROM Competitions WHERE ID=@ID;", new { ID = item.ID });
            }
        }

        /// <summary>
        /// Gets the competition data.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The param.</param>
        /// <returns>
        /// The competition data that matches the sql query.
        /// </returns>
        private IEnumerable<Competition> GetCompetitionData(string sql, dynamic param = null)
        {
            List<Competition> competitions = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                using (var multi = cn.QueryMultiple(sql, (object)param))
                {
                    IEnumerable<dynamic> possibleAnswers = multi.Read().ToList();
                    IEnumerable<dynamic> entrants = multi.Read().ToList();

                    competitions = multi.Read<dynamic, dynamic, Competition>((comp, user) =>
                    {
                        Competition competition = MapCompetition(comp, entrants, possibleAnswers);
                        competition.CreatedBy = MapUser(user);
                        return competition;
                    }).ToList();
                }
                cn.Close();
            }

            return competitions;
        }

        /// <summary>
        /// Maps the competition.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private static Competition MapCompetition(dynamic item, IEnumerable<dynamic> entrants, IEnumerable<dynamic> possibleAnswers)
        {
            var competition = new Competition()
            {
                ID = item.ID,
                ClosingDate = item.ClosingDate,
                CompetitionKey = item.CompetitionKey,
                CreatedDate = item.CreatedDate,
                Question = item.Question
            };

            competition.SetCompetitionState(CompetitionStateFactory.GetInstance((CompetitionStatus)item.Status));

            if (possibleAnswers != null)
            {
                foreach (var m in possibleAnswers.Where(p => p.CompetitionID == competition.ID))
                {
                    competition.PossibleAnswers.Add(new PossibleAnswer(m.IsCorrectAnswer, (CompetitionAnswer)m.AnswerKey, m.AnswerText));
                }
            }

            if (entrants != null)
            {
                foreach (var entrant in entrants.Where(e => e.CompetitionID == competition.ID))
                {
                    Entrant mapped = MapEntrant(entrant);
                    competition.AddEntrant(mapped);

                    // set the winner
                    if (mapped.ID == item.WinnerID)
                    {
                        competition.Winner = mapped;
                    }
                }
            }

            return competition;
        }

        /// <summary>
        /// Maps the user.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private static User MapUser(dynamic item)
        {
            var user = new User 
            {
                ID = item.ID, 
                Username = item.Username
            };

            return user;
        }

        /// <summary>
        /// Maps the entrant.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// An entrant item.
        /// </returns>
        private static Entrant MapEntrant(dynamic item)
        {
            if ((object) item == null) return null;

            IEntrantContact contact = EntrantContactFactory.GetInstance((EntrantContactType)item.ContactType);
            contact.Contact = item.ContactDetail;

            var entrant = new Entrant()
            {
                ID = item.ID,
                Answer = (CompetitionAnswer)item.AnswerKey,
                EntryDate = item.EntryDate,
                Source = (EntrantSource)item.Source,
                Contact = contact
            };

            return entrant;
        }

        /// <summary>
        /// Saves the possible answers.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="updating">if set to <c>true</c> [updating].</param>
        private static void SavePossibleAnswers(IDbConnection cn, Competition item, bool updating = false)
        { 
            const string insertSql = "INSERT INTO PossibleAnswers VALUES (@CompetitionID, @AnswerKey, @AnswerText, @IsCorrectAnswer)";
            const string updateSql = "UPDATE PossibleAnswers SET AnswerText=@AnswerText, IsCorrectAnswer=@IsCorrectAnswer WHERE CompetitionID = @CompetitionID AND AnswerKey=@AnswerKey";

            foreach (var answer in item.PossibleAnswers.Answers)
            {
                var param = new
                {
                    CompetitionID = item.ID,
                    AnswerKey = answer.AnswerKey,
                    AnswerText = answer.AnswerText,
                    IsCorrectAnswer = answer.IsCorrectAnswer
                };

                cn.Execute((updating) ? updateSql : insertSql, param);
            }
        }

        /// <summary>
        /// Saves the entrants.
        /// </summary>
        /// <param name="item">The item.</param>
        private static void SaveEntrants(IDbConnection cn, Competition item)
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
