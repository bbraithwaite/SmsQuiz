using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BB.SmsQuiz.ReadModel.Competition;
using BB.SmsQuiz.Services;
using BB.SmsQuiz.Services.Messaging.Competition;
using BB.SmsQuiz.Web.Controllers;
using BB.SmsQuiz.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BB.SmsQuiz.Web.Tests
{
    [TestClass]
    public class ComeptitionControllerTests
    {
        [TestMethod]
        public void IndexReturnsViewModelWithCompetitions()
        {
            // Arrange
            Mock<ICompetitionService> service = new Mock<ICompetitionService>();
            service.Setup(s => s.GetCompetitions()).Returns(GetResponseStub());
            CompetitionController controller = new CompetitionController(service.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(CompetitionViewModel));
            Assert.AreEqual(10, ((CompetitionViewModel)result.Model).Competitions.Count());
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