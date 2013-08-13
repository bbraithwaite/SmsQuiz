using System;
using BB.SmsQuiz.ApiModel.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;

namespace BB.SmsQuiz.Api.IntegrationTests
{
    [TestClass]
    public class AuthenticationTests
    {
        [TestMethod]
        public void Valid_Login_Returns_OK_Status_And_Authentication_Token()
        {
            // Arrange
            var item = new PostAuthentication() { Password = "admin", Username = "admin"};

            // Act
            var result = ApiClient.GetClient().PostAsJsonAsync("authentication", item).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("teddybear", result.Content.ReadAsAsync<string>().Result);
        }

        [TestMethod]
        public void Invalid_Login_Returns_Status()
        {
            // Arrange
            var item = new PostAuthentication() { Password = "invalid", Username = "admin" };

            // Act
            var result = ApiClient.GetClient().PostAsJsonAsync("authentication", item).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
