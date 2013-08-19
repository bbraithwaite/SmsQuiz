// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CloseCompetitionUnitTests.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.ApiModel.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Api.IntegrationTests
{
    /// <summary>
    /// Tests for closing a competition.
    /// </summary>
    [TestClass]
    public class CloseCompetitionUnitTests : BaseTest
    {
        /// <summary>
        /// Creates a competition and closes it.
        /// </summary>
        [TestMethod]
        public void Close_Competition_Returns_OK()
        {
            // Arrange
            var competition = new PostCompetition()
            {
                Answers = new[] { "Man Utd", "Man City", "Chelsea", "Arsenal" },
                ClosingDate = DateTime.Now.AddMonths(-1),
                CompetitionKey = RandomGenerator.GetRandomString(10),
                CorrectAnswerKey = 1,
                Question = "Who won the 2012 Premier League?"
            };

            Assert.AreEqual(
                HttpStatusCode.Created,
                Client.PostAsJsonAsync(Resources.Competitions, competition).Result.StatusCode,
                "POSTCompetition not OK.");

            // Act
            var response =
                Client.PutAsJsonAsync(
                    string.Format("{0}/{1}", Resources.CloseCompetition, competition.CompetitionKey.ToLower()), new { })
                      .Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "PUT CloseCompetition not OK.");
        }
    }
}