using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BB.SmsQuiz.Services;
using BB.SmsQuiz.Services.Messaging.Competition;
using BB.SmsQuiz.Web.Controllers;
using BB.SmsQuiz.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BB.SmsQuiz.Services.Messaging;
using System;

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
            service.Setup(s => s.GetCompetitions(It.IsAny<GetCompetitionsRequest>())).Returns(GetCompetitionsResponseStub());
            CompetitionsController controller = new CompetitionsController(service.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(CompetitionsViewModel));
            Assert.AreEqual(10, ((CompetitionsViewModel)result.Model).Competitions.Count());
        }

        [TestMethod]
        public void CreatePostRedirectsToIndex()
        {
            // Arrange
            Mock<ICompetitionService> service = new Mock<ICompetitionService>();
            service.Setup(s => s.CreateCompetition(It.IsAny<CreateCompetitionRequest>()))
                                    .Returns(new CreateCompetitionResponse() { Status = ResponseStatus.OK });

            CompetitionsController controller = new CompetitionsController(service.Object);
            CompetitionViewModel model = new CompetitionViewModel();

            // Act
            var result = controller.Create(model) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreatePostReturnsViewWhenInvalid()
        {
            // Arrange
            Mock<ICompetitionService> service = new Mock<ICompetitionService>();
            service.Setup(s => s.CreateCompetition(It.IsAny<CreateCompetitionRequest>()))
                                    .Returns(new CreateCompetitionResponse() { Status = ResponseStatus.Invalid });

            CompetitionsController controller = new CompetitionsController(service.Object);
            CompetitionViewModel model = new CompetitionViewModel();

            // Act
            var result = controller.Create(model) as ViewResult;

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void DetailsReturnsViewModel()
        {
            // Arrange
            Mock<ICompetitionService> service = new Mock<ICompetitionService>();
            service.Setup(s => s.GetCompetition(It.IsAny<GetCompetitionRequest>()))
                                    .Returns(new GetCompetitionResponse() { Status = ResponseStatus.OK });

            CompetitionsController controller = new CompetitionsController(service.Object);
            CompetitionViewModel model = new CompetitionViewModel();

            // Act
            var result = controller.Details(Guid.NewGuid()) as ViewResult;

            // Assert
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(CompetitionViewModel));
        }

        private static GetCompetitionsResponse GetCompetitionsResponseStub()
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