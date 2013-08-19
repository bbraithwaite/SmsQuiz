// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionTests.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.ApiModel.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Api.IntegrationTests
{
    /// <summary>
    /// Tests for managing competitions (CRUD operations).
    /// </summary>
    [TestClass]
    public class CompetitionTests : BaseTest
    {
        /// <summary>
        /// The competition_ cru d_ tests.
        /// </summary>
        [TestMethod]
        public void Competition_CRUD_Tests()
        {
            GetCompetition competition = Post();
            Get();
            Get(competition);
            Put(competition);
            Delete(competition);
        }

        /// <summary>
        /// The get_ competition_ with_ invalid_ id_ throws_ not_ found_ exception.
        /// </summary>
        [TestMethod]
        public void Get_Competition_With_Invalid_Id_Throws_Not_Found_Exception()
        {
            // Act
            var response = Client.GetAsync(GetUrRequestUri(new GetCompetition())).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// The clean up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            Client.Dispose();
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <returns>
        /// The <see cref="GetCompetition"/>.
        /// </returns>
        private GetCompetition Post()
        {
            // Arrange
            var competition = new PostCompetition()
                {
                    Answers = new[] {"A", "B", "C", "D"}, 
                    ClosingDate = DateTime.Now.AddMonths(1), 
                    CompetitionKey = RandomGenerator.GetRandomString(10), 
                    CorrectAnswerKey = 1, 
                    Question = "Test Question"
                };

            // Act
            var response = Client.PostAsJsonAsync(Resources.Competitions, competition).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, "POST competition not ok.");

            return response.Content.ReadAsAsync<GetCompetition>().Result;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        private void Delete(GetCompetition competition)
        {
            // Act
            var response = Client.DeleteAsync(GetUrRequestUri(competition)).Result;
            var deletedStatus = Client.GetAsync(GetUrRequestUri(competition)).Result.StatusCode;

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, "DELETE Competition not No Content.");
            Assert.AreEqual(HttpStatusCode.NotFound, deletedStatus, "DELETE Competition not deleted.");
        }

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        private void Put(GetCompetition competition)
        {
            // Arrange
            var putCompetition = new PutCompetition()
                {
                    Answers = new[] {"A", "B", "C", "D"}, 
                    ClosingDate = DateTime.Now.AddMonths(1), 
                    CorrectAnswerKey = 1, 
                    Question = "Test Question"
                };

            // Act
            var response = Client.PutAsJsonAsync(GetUrRequestUri(competition), putCompetition).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "PUT Competition not OK.");
            Assert.AreEqual(putCompetition.Question, Get(competition).Question, "PUT Competition not updated.");
        }

        /// <summary>
        /// The get.
        /// </summary>
        private void Get()
        {
            var list = Client
                .GetAsync(Resources.Competitions)
                .Result.Content.ReadAsAsync<IEnumerable<GetCompetition>>()
                .Result;

            Assert.IsTrue(list.Any());
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        /// <returns>
        /// The <see cref="GetCompetition"/>.
        /// </returns>
        private GetCompetition Get(GetCompetition competition)
        {
            // Act
            var response = Client.GetAsync(GetUrRequestUri(competition)).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Get competition by ID not OK.");

            return response.Content.ReadAsAsync<GetCompetition>().Result;
        }

        /// <summary>
        /// The get ur request uri.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetUrRequestUri(GetCompetition competition)
        {
            return string.Format("{0}/{1}", Resources.Competitions, competition.Id);
        }
    }
}