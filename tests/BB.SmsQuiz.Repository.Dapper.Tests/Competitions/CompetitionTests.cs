using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using BB.SmsQuiz.Model.Competitions.Entrants;
using BB.SmsQuiz.Model.Users;
using BB.SmsQuiz.Repository.Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Repository.Tests.Competitions
{
    [TestClass]
    public class CompetitionTests
    {
        ICompetitionRepository _competitionRepository;
        private IUserRepository _userRepository;

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
            _competitionRepository = new CompetitionRepository();
            _userRepository = new UserRepository();
            InitialiseParameters();
        }

        /// <summary>
        /// Competitions the crud.
        /// </summary>
        [TestMethod]
        public void CompetitionCrud()
        {
            Guid deletingID = Create();
            Delete(deletingID);

            Guid newID = Create();
            GetByID(newID);
            GetAll();
            Update(newID);
        }

        private void InitialiseParameters()
        {
            CLOSING_DATE = new DateTime(2013, 1, 1);
            COMPETITION_KEY = StringExtensions.GetRandomString(5);
            CREATED_DATE = DateTime.Now;
            CREATED_BY_ID = GetNewUserID();
            QUESTION = "Who is Luke Skywalkers father?";
            OPEN_STATE = CompetitionStatus.Open;
            CLOSED_STATE = CompetitionStatus.Closed;

            POSSIBLE_ANSWERS = new PossibleAnswers();
            POSSIBLE_ANSWERS.Add(CompetitionAnswer.A, "Darth Vader", isCorrectAnswer: true);
            POSSIBLE_ANSWERS.Add(CompetitionAnswer.B, "Obi Wan Kenobi");
            POSSIBLE_ANSWERS.Add(CompetitionAnswer.C, "George Lucas");
            POSSIBLE_ANSWERS.Add(CompetitionAnswer.D, "Walt Disney");
        }

        private Guid GetNewUserID()
        {
            var user = new User()
            {
                Username = StringExtensions.GetRandomString(10),
                Password = new EncryptedString(Encoding.UTF8.GetBytes(StringExtensions.GetRandomString(10)))
            };

            _userRepository.Add(user);

            return user.ID;
        }

        private Guid Create()
        {
            // Arrange
            var competition = new Competition
                {
                    ClosingDate = CLOSING_DATE,
                    CompetitionKey = COMPETITION_KEY,
                    CreatedBy = new User() { ID = CREATED_BY_ID },
                    CreatedDate = CREATED_DATE,
                    Question = QUESTION,
                    PossibleAnswers = POSSIBLE_ANSWERS
                };

            // Act
            _competitionRepository.Add(competition);

            // Assert
            Assert.AreNotEqual(Guid.Empty, competition.ID, "Creating new record does not return id");

            return competition.ID;
        }

        private void Update(Guid id)
        {
            // Arrange
            string newCompetitionKey = StringExtensions.GetRandomString(5);
            Competition competition = _competitionRepository.FindByID(id);
            competition.CompetitionKey = newCompetitionKey;
            competition.SetCompetitionState(new ClosedState());

            IEntrantContact contact = EntrantContactFactory.GetInstance(EntrantContactType.Email);
            contact.Contact = "test@example.com";

            var entrant = new Entrant
                {
                    Answer = CompetitionAnswer.A,
                    EntryDate = DateTime.Now,
                    Source = EntrantSource.Email,
                    Contact = contact
                };

            competition.AddEntrant(entrant);

            // Act
            _competitionRepository.Update(competition);

            Entrant winner = competition.Entrants.First();
            competition.Winner = winner;
            Guid winnerID = winner.ID;

            _competitionRepository.Update(competition);

            Competition updatedCompetition = _competitionRepository.FindByID(id);

            // Assert
            Assert.AreEqual(newCompetitionKey, updatedCompetition.CompetitionKey, "Record is not updated.");
            Assert.AreEqual(winnerID, updatedCompetition.Winner.ID, "Winner is not updated.");
            Assert.AreEqual(CLOSED_STATE, updatedCompetition.State.Status, "Competition status is not updated.");
        }

        private void GetAll()
        {
            // Act
            IEnumerable<Competition> items = _competitionRepository.FindAll();

            // Assert
            Assert.IsTrue(items.Any(), "GetAll returned no items.");
            Assert.AreEqual(4, items.First().PossibleAnswers.Answers.Count());
        }

        private void GetByID(Guid id)
        {
            // Act
            Competition competition = _competitionRepository.FindByID(id);

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

        private void Delete(Guid id)
        {
            // Arrange
            Competition competition = _competitionRepository.FindByID(id);

            // Act
            _competitionRepository.Remove(competition);
            competition = _competitionRepository.FindByID(id);

            // Assert
            Assert.IsNull(competition, "Record is not deleted.");
        }
    }
}