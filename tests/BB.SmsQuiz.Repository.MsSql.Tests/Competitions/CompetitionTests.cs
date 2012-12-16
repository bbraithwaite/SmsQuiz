using System;
using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using BB.SmsQuiz.Model.Competitions.Entrants;
using BB.SmsQuiz.Model.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Repository.Tests.Competitions
{
    [TestClass]
    public class CompetitionTests
    {
        ICompetitionRepository _repository;

        private DateTime CLOSING_DATE;
        private string COMPETITION_KEY;
        private DateTime CREATED_DATE;
        private Guid CREATED_BY_ID;
        private string QUESTION;
        private CompetitionStatus OPEN_STATE;
        private CompetitionStatus CLOSED_STATE;
        private PossibleAnswers POSSIBLE_ANSWERS;

        /// <summary>
        /// Sets up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _repository = (ICompetitionRepository)RepositoryFactory.Instance("CompetitionRepository");
            InitialiseParameters();

            // Create dependencies
            // Create new user here.
        }

        /// <summary>
        /// Competitions the crud.
        /// </summary>
        [TestMethod]
        public void CompetitionCrud()
        {
            Guid newID = Create();
            GetByID(newID);
            GetAll();
            GetAllOpenCompetitions();
            Update(newID);
            GetAllClosedCompetitions();
            Delete(newID);
        }

        private void InitialiseParameters()
        {
            CLOSING_DATE = new DateTime(2013, 1, 1);
            COMPETITION_KEY = "SW";
            CREATED_BY_ID = new Guid("05D5FD46-263E-E211-BFBA-1040F3A7A3B1");
            CREATED_DATE = DateTime.Now;
            QUESTION = "Who is Luke Skywalkers father?";
            OPEN_STATE = CompetitionStatus.Open;
            CLOSED_STATE = CompetitionStatus.Closed;

            POSSIBLE_ANSWERS = new PossibleAnswers();
            POSSIBLE_ANSWERS.Add(CompetitionAnswer.A, "Darth Vader", isCorrectAnswer: true);
            POSSIBLE_ANSWERS.Add(CompetitionAnswer.B, "Obi Wan Kenobi");
            POSSIBLE_ANSWERS.Add(CompetitionAnswer.C, "George Lucas");
            POSSIBLE_ANSWERS.Add(CompetitionAnswer.D, "Walt Disney");
        }

        private void GetAllClosedCompetitions()
        {
            // Act
            IEnumerable<Competition> items = _repository.Find(c => c.Status == CompetitionStatus.Closed);

            // Assert
            Assert.AreEqual(0, items.Count());
        }

        private void GetAllOpenCompetitions()
        {
            // Act
            IEnumerable<Competition> items = _repository.Find(c => c.Status == CompetitionStatus.Open);

            // Assert
            Assert.IsTrue(items.Count() > 0);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>The id of the new record.</returns>
        private Guid Create()
        {
            // Arrange
            Competition competition = new Competition();
            competition.ClosingDate = CLOSING_DATE;
            competition.CompetitionKey = COMPETITION_KEY;
            competition.CreatedBy = new User() { ID = CREATED_BY_ID };
            competition.CreatedDate = CREATED_DATE;
            competition.Question = QUESTION;
            competition.PossibleAnswers = POSSIBLE_ANSWERS;

            // Act
            _repository.Add(competition);

            // Assert
            Assert.AreNotEqual(Guid.Empty, competition.ID, "Creating new record does not return id");

            return competition.ID;
        }

        /// <summary>
        /// Updates the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        private void Update(Guid id)
        {
            // Arrange
            Competition competition = _repository.FindByID(id);
            competition.CompetitionKey = "SWQ";
            competition.SetCompetitionState(new ClosedState());

            IEntrantContact contact = EntrantContactFactory.GetInstance(EntrantContactType.Email);
            contact.Contact = "test@example.com";

            Entrant entrant = new Entrant();
            entrant.Answer = CompetitionAnswer.A;
            entrant.EntryDate = DateTime.Now;
            entrant.Source = EntrantSource.Email;
            entrant.Contact = contact;

            competition.AddEntrant(entrant);

            // Act
            _repository.Update(competition);

            Entrant winner = competition.Entrants.First();
            competition.Winner = winner;
            Guid winnerID = winner.ID;

            _repository.Update(competition);

            Competition updatedCompetition = _repository.FindByID(id);

            // Assert
            Assert.AreEqual("SWQ", updatedCompetition.CompetitionKey, "Record is not updated.");
            Assert.AreEqual(winnerID, updatedCompetition.Winner.ID, "Winner is not updated.");
            Assert.AreEqual(CLOSED_STATE, updatedCompetition.State.Status, "Competition status is not updated.");
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        private void GetAll()
        {
            // Act
            IEnumerable<Competition> items = _repository.FindAll();

            // Assert
            Assert.IsTrue(items.Count() > 0, "GetAll returned no items.");
            Assert.AreEqual(4, items.First().PossibleAnswers.Answers.Count());
        }

        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="id">The id of the competition.</param>
        private void GetByID(Guid id)
        {
            // Act
            Competition competition = _repository.FindByID(id);

            // Assert
            Assert.IsNotNull(competition, "GetByID returned null.");
            Assert.AreEqual(id, competition.ID);
            Assert.AreEqual(CLOSING_DATE.Date, competition.ClosingDate.Date);
            Assert.AreEqual(COMPETITION_KEY, competition.CompetitionKey);
            Assert.AreEqual(CREATED_BY_ID, competition.CreatedBy.ID);
            Assert.AreEqual(CREATED_DATE.Date, competition.CreatedDate.Date);
            Assert.AreEqual(QUESTION, competition.Question);
            Assert.AreEqual(OPEN_STATE, competition.State.Status);
            Assert.AreEqual(4, competition.PossibleAnswers.Answers.Count());
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        private void Delete(Guid id)
        {
            // Arrange
            Competition competition = _repository.FindByID(id);

            // Act
            _repository.Remove(competition);
            competition = _repository.FindByID(id);

            // Assert
            Assert.IsNull(competition, "Record is not deleted.");
        }
    }
}