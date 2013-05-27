using System;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Controllers;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Model.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BB.SmsQuiz.Api.Tests
{
    [TestClass]
    public class CompetitionControllerTests
    {
        [TestMethod]
        public void Create_Competition_Returns_Status_Code_And_New_ID()
        {
            // Arrange
            var repo = new Mock<ICompetitionRepository>();
            repo.Setup(s => s.Add(It.IsAny<Competition>()));

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<PostCompetition, Competition>(It.IsAny<PostCompetition>())).Returns(new Competition());

            var item = new PostCompetition()
            {
                Answers = new[] { "A", "B", "C", "D" },
                ClosingDate = DateTime.Now.AddMonths(1),
                CompetitionKey = "ABC",
                CorrectAnswerKey = 1,
                Question = "Test Question",
                UserId = new Guid("d8b6d842-8694-e211-be86-000c29e0f1a3")
            };

            var controller = new CompetitionsController(repo.Object, mapper.Object, null);
            Common.RegisterContext(controller, "competition");

            // Act
            HttpResponseMessage response = controller.Post(item);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
