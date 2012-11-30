using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Services.Implementation;
using BB.SmsQuiz.Services.Messaging.Competition;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BB.SmsQuiz.Infrastructure.Domain;
using BB.SmsQuiz.ReadModel.Competition;
using Moq;
using System;

namespace BB.SmsQuiz.Services.Tests
{
    [TestClass]
    public class CompetitionServiceTests
    {
        [TestMethod]
        public void GetCompetitionsReturnsAListOfOpenCompetitions()
        {
            // Arrange
            Mock<ICompetitionRepository> respository = new Mock<ICompetitionRepository>();
            respository.Setup(r => r.GetCompetitiions()).Returns(GetStubCompetitions());

            Mock<IMapper> mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<IEnumerable<Competition>, IEnumerable<CompetitionItem>>(It.IsAny<IEnumerable<Competition>>()))
                               .Returns(GetStubCompetitionItems);

            CompetitionService service = new CompetitionService(respository.Object, mapper.Object);

            // Act
            GetCompetitionsResponse response = service.GetCompetitions();

            // Assert
            Assert.AreEqual(10, response.Competitions.Count());
            Assert.AreEqual("Question #1", response.Competitions.ElementAt(0).Question);
            Assert.AreEqual("Question #10", response.Competitions.ElementAt(9).Question);
        }

        private static List<Competition> GetStubCompetitions()
        {
            List<Competition> list = new List<Competition>();

            for (int i = 1; i < 11; i++)
            {
                list.Add(new Competition() { Question = "Question #" + i.ToString() });
            }

            return list;
        }

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