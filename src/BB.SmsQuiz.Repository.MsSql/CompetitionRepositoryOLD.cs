//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Common;
//using BB.SmsQuiz.Model.Competitions;
//using BB.SmsQuiz.Model.Entrants;
//using BB.SmsQuiz.Model.Users;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using BB.SmsQuiz.Model.Competitions.States;
//using System.Linq;

//namespace BB.SmsQuiz.Repository.MsSql
//{
//    /// <summary>
//    /// A competition repository of a manual sql implementation.
//    /// </summary>
//    public sealed class CompetitionRepositoryOLD : ICompetitionRepository
//    {
//        /// <summary>
//        /// Gets the competitions.
//        /// </summary>
//        /// <returns>A list of competitions</returns>
//        public IQueryable<Competition> GetAll()
//        {
//            List<Competition> competitions = new List<Competition>();
//            Database db = DatabaseFactory.CreateDatabase();
//            DbCommand dbCommand = db.GetSqlStringCommand(SelectColsPrefix);

//            using (IDataReader reader = db.ExecuteReader(dbCommand))
//            {
//                while (reader.Read())
//                {
//                    competitions.Add(MapFromReader(reader));
//                }
//            }

//            return competitions.AsQueryable<Competition>();
//        }

//        /// <summary>
//        /// Creates the competition.
//        /// </summary>
//        /// <param name="competition">The competition.</param>
//        public void Add(Competition competition)
//        {
//            List<Competition> competitions = new List<Competition>();

//            Database db = DatabaseFactory.CreateDatabase();
//            DbCommand cmd = db.GetSqlStringCommand(
//                "INSERT INTO Competitions (ClosingDate, CompetitionKey, CreatedDate, Question, [Status], createdByID, WinnerID) OUTPUT inserted.ID VALUES (@ClosingDate, @CompetitionKey, @CreatedDate, @Question, @Status, @createdByID, @WinnerID)");

//            MapProperties(db, cmd, competition);

//            competition.ID = (Guid)db.ExecuteScalar(cmd);
//        }

//        /// <summary>
//        /// Gets the competition.
//        /// </summary>
//        /// <param name="id">The id.</param>
//        /// <returns>The competition.</returns>
//        public Competition GetByID(Guid id)
//        {
//            List<Competition> competitions = new List<Competition>();
//            Database db = DatabaseFactory.CreateDatabase();
//            DbCommand cmd = db.GetSqlStringCommand(SelectColsPrefix + " WHERE ID=@ID");

//            db.AddInParameter(cmd, "@ID", DbType.Guid, id);

//            Competition competition = null;

//            using (IDataReader reader = db.ExecuteReader(cmd))
//            {
//                while (reader.Read())
//                {
//                    competition = MapFromReader(reader);
//                }
//            }

//            return competition;
//        }

//        /// <summary>
//        /// Updates the competition.
//        /// </summary>
//        /// <param name="competition">The competition.</param>
//        public void Update(Competition competition)
//        {
//            List<Competition> competitions = new List<Competition>();

//            Database db = DatabaseFactory.CreateDatabase();
//            DbCommand cmd = db.GetSqlStringCommand(
//                "UPDATE Competitions SET ClosingDate=@ClosingDate, CompetitionKey=@CompetitionKey, CreatedDate=@CreatedDate, Question=@Question, Status=@Status, CreatedByID=@CreatedByID, WinnerID=@WinnerID WHERE ID=@ID");

//            db.AddInParameter(cmd, "@ID", DbType.Guid, competition.ID);
//            MapProperties(db, cmd, competition);

//            db.ExecuteNonQuery(cmd);
//        }

//        /// <summary>
//        /// Deletes the competition.
//        /// </summary>
//        /// <param name="id">The id.</param>
//        public void Remove(Competition competition)
//        {
//            Database db = DatabaseFactory.CreateDatabase();
//            DbCommand cmd = db.GetSqlStringCommand("DELETE FROM Competitions WHERE ID=@ID");
//            db.AddInParameter(cmd, "@ID", DbType.Guid, competition.ID);
//            db.ExecuteNonQuery(cmd);
//        }

//        /// <summary>
//        /// Maps the properties.
//        /// </summary>
//        /// <param name="db">The db.</param>
//        /// <param name="cmd">The CMD.</param>
//        /// <param name="competition">The competition.</param>
//        private void MapProperties(Database db, DbCommand cmd, Competition competition)
//        {
//            db.AddInParameter(cmd, "@closingDate", DbType.DateTime, competition.ClosingDate);
//            db.AddInParameter(cmd, "@competitionKey", DbType.String, competition.CompetitionKey);
//            db.AddInParameter(cmd, "@createdDate", DbType.String, competition.CreatedDate);
//            db.AddInParameter(cmd, "@question", DbType.String, competition.Question);
//            db.AddInParameter(cmd, "@status", DbType.Byte, competition.State.Status);
//            db.AddInParameter(cmd, "@createdByID", DbType.Guid, competition.CreatedBy.ID);
//            db.AddInParameter(cmd, "@winnerID", DbType.Guid, competition.Winner != null ? competition.Winner.ID : Guid.Empty);
//        }

//        /// <summary>
//        /// Maps from reader.
//        /// </summary>
//        /// <param name="reader">The reader.</param>
//        /// <returns>The mapped competition.</returns>
//        private Competition MapFromReader(IDataReader reader)
//        {
//            Competition competition = new Competition();
//            competition.ClosingDate = (DateTime)reader["closingDate"];
//            competition.CompetitionKey = reader["competitionKey"].ToString();
//            competition.CreatedDate = (DateTime)reader["createdDate"];
//            competition.ID = (Guid)reader["ID"];
//            competition.Question = reader["question"].ToString();

//            //TODO: implement userID
//            competition.CreatedBy = new User() { ID = (Guid)reader["createdByID"] };

//            //TODO: implement winnerID
//            competition.Winner = new Entrant() { ID = (Guid)reader["winnerID"] };

//            //TODO: implement status
//            CompetitionStatus status = (CompetitionStatus)Enum.Parse(typeof(CompetitionStatus), reader["status"].ToString());
//            competition.SetCompetitionState(CompetitionStateFactory.GetState(status));

//            return competition;
//        }
//    }
//}