// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnterCompetitionUnitTests.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.ApiModel.Competitions;
using BB.SmsQuiz.ApiModel.EnterCompetition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Api.IntegrationTests
{
    /// <summary>
    /// Tests for entering a competition.
    /// </summary>
    [TestClass]
    public class EnterCompetitionUnitTests : BaseTest
    {
        /// <summary>
        /// Creates a new competition, and posts an entry to that competition.
        /// </summary>
        [TestMethod]
        public void Enter_Competition_Returns_OK_Status()
        {
            // #1 Create Competition
            var competition = new PostCompetition()
                {
                    Answers = new[] { "A", "B", "C", "D" }, 
                    ClosingDate = DateTime.Now.AddMonths(1), 
                    CompetitionKey = RandomGenerator.GetRandomString(10), 
                    CorrectAnswerKey = 1, 
                    Question = "Test Question", 
                    UserId = GetUser().ID
                };

            Assert.AreEqual(
                HttpStatusCode.Created, 
                Client.PostAsJsonAsync(Resources.Competitions, competition).Result.StatusCode, 
                "POSTCompetition not OK.");

            // #2 Enter Competition
            var enterCompetition = new PostEnterCompetition()
                {
                    CompetitionKey = competition.CompetitionKey, 
                    ContactType = "Sms", 
                    Answer = "A", 
                    Contact = "00000111222", 
                    Source = "Sms"
                };

            Assert.AreEqual(
                HttpStatusCode.Created, 
                Client.PostAsJsonAsync(Resources.EnterCompetition, enterCompetition).Result.StatusCode, 
                "POST EnterCompetition not OK.");
        }
    }
}