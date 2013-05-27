using System;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.ApiModel.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.IntegrationTests
{
    [TestClass]
    public class CompetitionTests
    {
        [TestMethod]
        public void Create_Competition_Returns_Status_Code_And_New_ID()
        {
            // Arrange
            var item = new PostCompetition()
            {
                Answers = new[] { "A", "B", "C", "D" },
                ClosingDate = DateTime.Now.AddMonths(1),
                CompetitionKey = RandomGenerator.GetRandomKey(3),
                CorrectAnswerKey = 1,
                Question = "Test Question",
                UserId = new Guid("d8b6d842-8694-e211-be86-000c29e0f1a3")
            };

            // Act
            var result = ApiClient.GetClient().PostAsJsonAsync("competitions", item).Result;
            var createdItem = result.Content.ReadAsAsync<GetCompetition>().Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.IsNotNull(createdItem);
        }

        internal string GetErrors(HttpResponseMessage response)
        {
            string errorString = string.Empty;

            foreach (var item in response.Content.ReadAsAsync<dynamic>().Result.Items)
            {
                errorString += string.Format("{0} : {1}", item.PropertyName.Value, item.Message.Value);
            }

            return errorString;
        }
    }
}
