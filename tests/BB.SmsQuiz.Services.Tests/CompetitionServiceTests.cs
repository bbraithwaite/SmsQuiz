using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Entrants;
using BB.SmsQuiz.Services.Implementation;
using BB.SmsQuiz.Services.Messaging;
using BB.SmsQuiz.Services.Messaging.Competition;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BB.SmsQuiz.Services.Tests
{
    [TestClass]
    public class CompetitionServiceTests
    {
        /// <summary>
        /// Gets the competitions returns A list of open competitions.
        /// </summary>
        [TestMethod]
        public void GetCompetitionsReturnsAListOfOpenCompetitions()
        {
            // Arrange
            Mock<ICompetitionRepository> respository = GetRepositoryMock();
            Mock<IMapper> mapper = GetMapperMock();

            CompetitionService service = new CompetitionService(respository.Object, mapper.Object);

            GetCompetitionsRequest request = new GetCompetitionsRequest();
            request.Status = Model.Competitions.States.CompetitionStatus.Open;

            // Act
            GetCompetitionsResponse response = service.GetCompetitions(request);

            // Assert
            Assert.AreEqual(10, response.Competitions.Count());
            Assert.AreEqual("Question #1", response.Competitions.ElementAt(0).Question);
            Assert.AreEqual("Question #10", response.Competitions.ElementAt(9).Question);
        }

        /// <summary>
        /// Gets the competition returns the expected competition.
        /// </summary>
        [TestMethod]
        public void GetCompetitionReturnsTheExpectedCompetition()
        {
            // Arrange
            Mock<ICompetitionRepository> respository = GetRepositoryMock();
            Mock<IMapper> mapper = GetMapperMock();

            CompetitionService service = new CompetitionService(respository.Object, mapper.Object);

            GetCompetitionRequest request = new GetCompetitionRequest();
            request.ID = Guid.NewGuid();

            // Act
            GetCompetitionResponse response = service.GetCompetition(request);

            // Assert
            Assert.IsNotNull(response.Competition);
        }

        /// <summary>
        /// Creates the competition in valid state returns ok.
        /// </summary>
        [TestMethod]
        public void CreateCompetitionInValidStateReturnsOk()
        {
            // Arrange
            Mock<Competition> competition = new Mock<Competition>();
            competition.Setup(e => e.IsValid).Returns(true);

            Mock<IMapper> mapper = GetMapperMock(competition: competition);
            Mock<ICompetitionRepository> respository = GetRepositoryMock();

            CompetitionService service = new CompetitionService(respository.Object, mapper.Object);

            CreateCompetitionRequest request = new CreateCompetitionRequest();
            request.Competition = new CompetitionItem();

            // Act
            CreateCompetitionResponse response = service.CreateCompetition(request);

            // Assert
            Assert.AreEqual(ResponseStatus.OK, response.Status);
        }

        /// <summary>
        /// Creates the competition in invalid state returns invalid status.
        /// </summary>
        [TestMethod]
        public void CreateCompetitionInInvalidStateReturnsInvalidStatus()
        {
            // Arrange
            Mock<Competition> competition = new Mock<Competition>();
            competition.Setup(e => e.IsValid).Returns(false);
            competition.Setup(e => e.ValidationErrors).Returns(GetValidationErrorsStub());

            Mock<IMapper> mapper = GetMapperMock(competition: competition);
            Mock<ICompetitionRepository> respository = GetRepositoryMock();

            CompetitionService service = new CompetitionService(respository.Object, mapper.Object);

            CreateCompetitionRequest request = new CreateCompetitionRequest();
            request.Competition = new CompetitionItem();

            // Act
            CreateCompetitionResponse response = service.CreateCompetition(request);

            // Assert
            Assert.AreEqual(ResponseStatus.Invalid, response.Status);
            Assert.AreEqual(2, response.ValidationErrors.Count());
        }

        /// <summary>
        /// Enters the competition in valid state returns an ok status.
        /// </summary>
        [TestMethod]
        public void EnterCompetitionInValidStateReturnsAnOkStatus()
        {
            // Arrange
            Mock<Entrant> entrant = new Mock<Entrant>();
            entrant.Setup(e => e.IsValid).Returns(true);

            Mock<IMapper> mapper = GetMapperMock(entrant: entrant);
            Mock<ICompetitionRepository> respository = GetRepositoryMock();

            CompetitionService service = new CompetitionService(respository.Object, mapper.Object);

            EnterCompetitionRequest request = new EnterCompetitionRequest();
            request.Entrant = new EntrantItem();

            // Act
            EnterCompetitionResponse response = service.EnterCompetition(request);

            // Assert
            Assert.AreEqual(ResponseStatus.OK, response.Status);
        }

        /// <summary>
        /// Enters the competition in valid state returns an ok status.
        /// </summary>
        [TestMethod]
        public void EnterCompetitionInInvalidStateReturnsValidationErrors()
        {
            // Arrange
            Mock<Entrant> entrant = new Mock<Entrant>();
            entrant.Setup(e => e.IsValid).Returns(false);
            entrant.Setup(e => e.ValidationErrors).Returns(GetValidationErrorsStub());

            Mock<IMapper> mapper = GetMapperMock(entrant);
            Mock<ICompetitionRepository> respository = GetRepositoryMock();

            CompetitionService service = new CompetitionService(respository.Object, mapper.Object);

            EnterCompetitionRequest request = new EnterCompetitionRequest();
            request.Entrant = new EntrantItem();

            // Act
            EnterCompetitionResponse response = service.EnterCompetition(request);

            // Assert
            Assert.AreEqual(ResponseStatus.Invalid, response.Status);
            Assert.AreEqual(2, response.ValidationErrors.Count());
        }

        /// <summary>
        /// Gets the validation errors stub.
        /// </summary>
        /// <returns>Validation error stub.</returns>
        private static ValidationErrors GetValidationErrorsStub()
        {
            var errors = new ValidationErrors();
            errors.Add("property1", "Property 1 is required");
            errors.Add("property2", "Property 2 is required");
            return errors;
        }

        /// <summary>
        /// Gets the repository mock.
        /// </summary>
        /// <returns>Repository mocks.</returns>
        private static Mock<ICompetitionRepository> GetRepositoryMock()
        {
            Mock<ICompetitionRepository> repository = new Mock<ICompetitionRepository>();

            repository.Setup(r => r.FindAll())
                                .Returns(GetStubCompetitions().AsQueryable<Competition>());

            repository.Setup(r => r.FindByID(It.IsAny<Guid>()))
                                .Returns(new Competition());

            repository.Setup(r => r.Find(It.IsAny<Expression<Func<Competition, bool>>>()))
                                .Returns(new List<Competition>() { new Competition() { CompetitionKey = "" } });

            return repository;
        }

        /// <summary>
        /// Gets the mapper mock.
        /// </summary>
        /// <param name="entrant">The entrant.</param>
        /// <param name="competition">The competition.</param>
        /// <returns>Mapper mocks.</returns>
        private static Mock<IMapper> GetMapperMock(Mock<Entrant> entrant = null, Mock<Competition> competition = null)
        {
            Mock<IMapper> mapper = new Mock<IMapper>();

            mapper.Setup(m => m.Map<EntrantItem, Entrant>(It.IsAny<EntrantItem>()))
                                .Returns((entrant != null) ? entrant.Object : null);

            mapper.Setup(m => m.Map<IEnumerable<ValidationError>, IEnumerable<ValidationItem>>(It.IsAny<IEnumerable<ValidationError>>()))
                                .Returns(new List<ValidationItem>() { new ValidationItem(), new ValidationItem() });

            mapper.Setup(m => m.Map<IEnumerable<Competition>, IEnumerable<CompetitionItem>>(It.IsAny<IEnumerable<Competition>>()))
                               .Returns(GetStubCompetitionItems);

            mapper.Setup(m => m.Map<Competition, CompetitionItem>(It.IsAny<Competition>()))
                                .Returns(new CompetitionItem());

            mapper.Setup(m => m.Map<CompetitionItem, Competition>(It.IsAny<CompetitionItem>()))
                                .Returns((competition != null) ? competition.Object : null);

            return mapper;
        }

        /// <summary>
        /// Gets the stub competitions.
        /// </summary>
        /// <returns>Competition stubs.</returns>
        private static List<Competition> GetStubCompetitions()
        {
            List<Competition> list = new List<Competition>();

            for (int i = 1; i < 11; i++)
            {
                list.Add(new Competition() { Question = "Question #" + i.ToString() });
            }

            return list;
        }

        /// <summary>
        /// Gets the stub competition items.
        /// </summary>
        /// <returns>Competition stub items.</returns>
        private static List<CompetitionItem> GetStubCompetitionItems()
        {
            List<CompetitionItem> list = new List<CompetitionItem>();

            for (int i = 1; i < 11; i++)
            {
                list.Add(new CompetitionItem() { Question = "Question #" + i.ToString() });
            }

            return list;
        }
    }
}