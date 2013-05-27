using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.ApiModel.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BB.SmsQuiz.IntegrationTests
{
    [TestClass]
    public class UserTests
    {
        private HttpClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            _client = ApiClient.GetAuthenticatedClient();
        }

        [TestMethod]
        public void UsersControllerTests()
        {
            GetUser user = Post();
            Get();
            Get(user);
            Put(user);
            Delete(user);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _client.Dispose();
        }

        private void Delete(GetUser user)
        {
            // Act
            var response = _client.DeleteAsync(GetUrRequestUri(user)).Result;
            var deletedStatus = _client.GetAsync(GetUrRequestUri(user)).Result.StatusCode;

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, "DELETE User not No Content.");
            Assert.AreEqual(HttpStatusCode.NotFound, deletedStatus, "DELETE User not deleted.");
        }

        private void Put(GetUser user)
        {
            // Arrange
            user.Username = RandomGenerator.GetRandomKey(10);

            // Act
            var response = _client.PutAsJsonAsync(GetUrRequestUri(user), user).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "PUT User not OK.");
            Assert.AreEqual(user.Username, Get(user).Username, "PUT User not updated.");
        }

        private GetUser Get(GetUser user)
        {
            // Act
            var response = _client.GetAsync(GetUrRequestUri(user)).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Get user by ID not OK.");

            return response.Content.ReadAsAsync<GetUser>().Result;
        }

        private GetUser Post()
        {
            // Arrange
            var user = new PostUser()
            {
                Username = RandomGenerator.GetRandomKey(10),
                Password = RandomGenerator.GetRandomKey(10)
            };

            // Act
            var response = _client.PostAsJsonAsync("users", user).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, "POST user not ok.");

            return response.Content.ReadAsAsync<GetUser>().Result;
        }

        [TestMethod]
        public void Get_User_With_Invalid_Id_Throws_Not_Found_Exception()
        {
            // Act
            var response = _client.GetAsync(GetUrRequestUri(new GetUser())).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        private void Get()
        {
            var list = _client
             .GetAsync("users")
             .Result.Content.ReadAsAsync<IEnumerable<GetUser>>()
             .Result;

            Assert.IsTrue(list.Any());
        }

        private static string GetUrRequestUri(GetUser user)
        {
            return "users/" + user.ID;
        }
    }
}
