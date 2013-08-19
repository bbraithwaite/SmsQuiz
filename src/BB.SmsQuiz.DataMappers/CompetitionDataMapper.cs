// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionDataMapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;
using BB.SmsQuiz.Model.Competitions.States;
using Dapper;

namespace BB.SmsQuiz.DataMappers
{
    /// <summary>
    /// The competition data mapper.
    /// </summary>
    public class CompetitionDataMapper : AbstractDataMapper<Competition>, ICompetitionDataMapper
    {
        /// <summary>
        /// The insert sql.
        /// </summary>
        private const string insertSql =
            "INSERT INTO PossibleAnswers VALUES (@CompetitionID, @AnswerKey, @AnswerText, @IsCorrectAnswer)";

        /// <summary>
        /// The update sql.
        /// </summary>
        private const string updateSql =
            "UPDATE PossibleAnswers SET AnswerText=@AnswerText, IsCorrectAnswer=@IsCorrectAnswer WHERE CompetitionID = @CompetitionID AND AnswerKey=@AnswerKey";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        protected override string TableName
        {
            get { return "Competitions"; }
        }

        /// <summary>
        /// The find by status.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Competition> FindByStatus(CompetitionStatus status)
        {
            return
                GetCompetitionData(
                    "SELECT C.*, U.* FROM Competitions C INNER JOIN Users U ON C.CreatedByID = U.ID WHERE C.Status=@Status;", 
                    new { Status = status });
        }

        /// <summary>
        /// The find by competition key.
        /// </summary>
        /// <param name="competitionKey">
        /// The competition key.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Competition FindByCompetitionKey(string competitionKey)
        {
            var competition =
                GetCompetitionData(
                    "SELECT C.*, U.* FROM Competitions C INNER JOIN Users U ON C.CreatedByID = U.ID WHERE C.CompetitionKey=@CompetitionKey;", 
                    new { CompetitionKey = competitionKey }).SingleOrDefault();
            competition.PossibleAnswers = GetPossibleAnswers(competition.ID);
            LoadEntrants(competition);
            return competition;
        }

        /// <summary>
        /// The find by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Competition FindById(Guid id)
        {
            var competition =
                GetCompetitionData(
                    "SELECT C.*, U.* FROM Competitions C INNER JOIN Users U ON C.CreatedByID = U.ID WHERE C.ID=@ID;", 
                    new { ID = id }).SingleOrDefault();
            competition.PossibleAnswers = GetPossibleAnswers(competition.ID);
            LoadEntrants(competition);
            return competition;
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Insert(Competition item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = new
                    {
                        ClosingDate = item.ClosingDate, 
                        CompetitionKey = item.CompetitionKey, 
                        CreatedDate = item.CreatedDate, 
                        Question = item.Question, 
                        CreatedByID = item.CreatedBy.ID, 
                        WinnerID = (item.Winner != null) ? item.Winner.ID : Guid.Empty, 
                        Status = item.State.Status
                    };

                cn.Open();
                item.ID = cn.Query<Guid>(
                    "INSERT INTO Competitions (ClosingDate, CompetitionKey, CreatedDate, Question, CreatedByID, Status) OUTPUT inserted.ID VALUES (@ClosingDate, @CompetitionKey, @CreatedDate, @Question, @CreatedByID, @Status)", 
                    parameters).First();

                SavePossibleAnswers(cn, item);
            }
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Update(Competition item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = new
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

                cn.Open();
                cn.Execute(
                    "UPDATE Competitions SET ClosingDate=@ClosingDate, CompetitionKey=@CompetitionKey, CreatedDate=@CreatedDate, Question=@Question, CreatedByID=@CreatedByID, Status=@Status WHERE ID=@ID", 
                    parameters);

                SavePossibleAnswers(cn, item, updating: true);

                foreach (var entrant in item.Entrants.Where(e => e.ID == Guid.Empty))
                {
                    InsertEntrant(cn, entrant, item.ID);
                }
            }
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public override void Delete(Guid id)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(
                    "DELETE FROM PossibleAnswers WHERE CompetitionID=@ID;DELETE FROM Competitions WHERE ID=@ID;", 
                    new { ID = id });
            }
        }

        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public override Competition Map(dynamic result)
        {
            var competition = new Competition()
                {
                    ID = result.ID, 
                    ClosingDate = result.ClosingDate, 
                    CompetitionKey = result.CompetitionKey, 
                    CreatedDate = result.CreatedDate, 
                    Question = result.Question
                };

            competition.SetCompetitionState(CompetitionStateFactory.GetInstance((CompetitionStatus) result.Status));

            return competition;
        }

        /// <summary>
        /// The get competition data.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="param">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<Competition> GetCompetitionData(string sql, dynamic param = null)
        {
            List<Competition> competitions = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                using (var multi = cn.QueryMultiple(sql, (object) param))
                {
                    competitions = multi.Read<dynamic, dynamic, Competition>((comp, user) =>
                        {
                            Competition competition = Map(comp);
                            competition.CreatedBy = new UserDataMapper().Map(user);

                            return competition;
                        }).ToList();
                }
            }

            return competitions;
        }

        /// <summary>
        /// The get possible answers.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="PossibleAnswers"/>.
        /// </returns>
        private PossibleAnswers GetPossibleAnswers(Guid id)
        {
            var possibleAnswers = new PossibleAnswers();

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var result = cn.Query("SELECT * FROM PossibleAnswers WHERE CompetitionID=@CompetitionID", new { CompetitionID = id }).ToList();

                foreach (var row in result)
                {
                    possibleAnswers.Add(new PossibleAnswer(row.IsCorrectAnswer, (CompetitionAnswer)row.AnswerKey, row.AnswerText));
                }
            }

            return possibleAnswers;
        }

        /// <summary>
        /// The load entrants.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        private void LoadEntrants(Competition competition)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var result = cn.Query("SELECT * FROM Entrants WHERE CompetitionID=@CompetitionID", new { CompetitionID = competition.ID }).ToList();

                foreach (var row in result)
                {
                    competition.AddEntrant(MapEntrant(row));
                }
            }
        }

        /// <summary>
        /// Saves the possible answers.
        /// </summary>
        /// <param name="cn">
        /// The cn.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="updating">
        /// if set to <c>true</c> [updating].
        /// </param>
        private static void SavePossibleAnswers(IDbConnection cn, Competition item, bool updating = false)
        {
            foreach (var answer in item.PossibleAnswers.Answers)
            {
                var param = new
                {
                    CompetitionID = item.ID,
                    AnswerKey = answer.AnswerKey,
                    AnswerText = answer.AnswerText,
                    IsCorrectAnswer = answer.IsCorrectAnswer
                };

                cn.Execute(updating ? updateSql : insertSql, param);
            }
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
            if ((object)item != null)
            {
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

            return null;
        }

        /// <summary>
        /// The insert entrant.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        private static void InsertEntrant(IDbConnection cn, Entrant item, Guid competitionId)
        {
            var param = new
            {
                AnswerKey = item.Answer,
                EntryDate = item.EntryDate,
                Source = item.Source,
                ContactType = item.Contact.ContactType,
                ContactDetail = item.Contact.Contact,
                CompetitionID = competitionId
            };

            item.ID = cn.Query<Guid>(
                "INSERT INTO Entrants (AnswerKey, EntryDate, Source, ContactType, ContactDetail, CompetitionID) OUTPUT inserted.ID VALUES (@AnswerKey, @EntryDate, @Source, @ContactType, @ContactDetail, @CompetitionID)",
                param).First();
        }
    }
}