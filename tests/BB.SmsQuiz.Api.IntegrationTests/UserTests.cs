// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserTests.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.ApiModel.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.Api.IntegrationTests
{
    /// <summary>
    /// The user tests.
    /// </summary>
    [TestClass]
    public class UserTests : BaseTest
    {
        /// <summary>
        /// The user controller tests.
        /// </summary>
        [TestMethod]
        public void UserControllerTests()
        {
            GetUser user = Post();
            Get();
            Get(user);
            Put(user);
            Delete(user);
        }

        /// <summary>
        /// The get_ user_ with_ invalid_ id_ throws_ not_ found_ exception.
        /// </summary>
        [TestMethod]
        public void Get_User_With_Invalid_Id_Throws_Not_Found_Exception()
        {
            // Act
            var response = Client.GetAsync(GetUrRequestUri(new GetUser())).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        private void Delete(GetUser user)
        {
            // Act
            var response = Client.DeleteAsync(GetUrRequestUri(user)).Result;
            var deletedStatus = Client.GetAsync(GetUrRequestUri(user)).Result.StatusCode;

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, "DELETE User not No Content.");
            Assert.AreEqual(HttpStatusCode.NotFound, deletedStatus, "DELETE User not deleted.");
        }

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        private void Put(GetUser user)
        {
            // Arrange
            user.Username = RandomGenerator.GetRandomString(10);

            // Act
            var response = Client.PutAsJsonAsync(GetUrRequestUri(user), user).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "PUT User not OK.");
            Assert.AreEqual(user.Username, Get(user).Username, "PUT User not updated.");
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="GetUser"/>.
        /// </returns>
        private GetUser Get(GetUser user)
        {
            // Act
            var response = Client.GetAsync(GetUrRequestUri(user)).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Get user by ID not OK.");

            return response.Content.ReadAsAsync<GetUser>().Result;
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <returns>
        /// The <see cref="GetUser"/>.
        /// </returns>
        private GetUser Post()
        {
            // Arrange
            var user = new PostUser()
                {
                    Username = RandomGenerator.GetRandomString(10), 
                    Password = RandomGenerator.GetRandomString(10)
                };

            // Act
            var response = Client.PostAsJsonAsync(Resources.Users, user).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, "POST user not ok.");

            return response.Content.ReadAsAsync<GetUser>().Result;
        }

        /// <summary>
        /// The get.
        /// </summary>
        private void Get()
        {
            var list = Client
                .GetAsync(Resources.Users)
                .Result.Content.ReadAsAsync<IEnumerable<GetUser>>()
                .Result;

            Assert.IsTrue(list.Any());
        }

        /// <summary>
        /// The get ur request uri.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetUrRequestUri(GetUser user)
        {
            return string.Format("{0}/{1}", Resources.Users, user.ID);
        }
    }
}