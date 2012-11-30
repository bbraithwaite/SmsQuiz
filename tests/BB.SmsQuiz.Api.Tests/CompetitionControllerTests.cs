using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Api.Controllers;
using BB.SmsQuiz.ReadModel.Competition;
using BB.SmsQuiz.Services;
using BB.SmsQuiz.Services.Messaging.Competition;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BB.SmsQuiz.Api.Tests
{
    [TestClass]
    public class CompetitionControllerTests
    {
        [TestMethod]
        public void GetCompetitionReturnsItems()
        {
            // Arrange
            Mock<ICompetitionService> service = new Mock<ICompetitionService>();
            service.Setup(s => s.GetCompetitions()).Returns(GetResponseStub());
            CompetitionController controller = new CompetitionController(service.Object);

            // Act
            IEnumerable<CompetitionItem> response = controller.Get();

            // Assert
            Assert.AreEqual(10, response.Count());
        }

        private static GetCompetitionsResponse GetResponseStub()
        {
            GetCompetitionsResponse response = new GetCompetitionsResponse();

            List<CompetitionItem> list = new List<CompetitionItem>();

            for (int i = 1; i < 11; i++)
            {
                list.Add(new CompetitionItem() { Question = "Question #" + i.ToString() });
            }

            response.Competitions = list;

            return response;
        }
    }
}
