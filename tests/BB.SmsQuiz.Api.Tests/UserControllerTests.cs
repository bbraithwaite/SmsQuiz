using BB.SmsQuiz.Api.Controllers;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.ApiModel.Users;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Model.Users;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace BB.SmsQuiz.Api.Tests
{
    /// <summary>
    /// Summary description for UserControllerTests
    /// </summary>
    [TestClass]
    public class UserControllerTests : BaseTest
    {
        /// <summary>
        /// Ger valid user by id returns a result
        /// </summary>
        [TestMethod]
        public void Get_User_By_Id_Returns_User()
        {
            // Arrange
            var repo = new Mock<IUserRepository>();
            repo.Setup(s => s.FindByID(It.IsAny<Guid>())).Returns(GetUserResponse());

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<User, GetUser>(It.IsAny<User>())).Returns(new GetUser());

            var controller = new UsersController(repo.Object, mapper.Object, null);

            // Act
            var response = controller.Get(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// Ger valid user by id returns a result
        /// </summary>
        [TestMethod]
        public void Get_User_With_Invalid_Id_Throws_Not_Found_Exception()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<User, GetUser>(It.IsAny<User>())).Returns(new GetUser());

            var controller = new UsersController(new Mock<IUserRepository>().Object, mapper.Object, null);

            // Assert
            Assert.Throws<NotFoundException>(() => controller.Get(Guid.NewGuid()));
        }

        /// <summary>
        /// Get list of users returns results
        /// </summary>
        [TestMethod]
        public void Get_Users_Returns_Users_List()
        {
            // Arrange
            var repo = new Mock<IUserRepository>();
            repo.Setup(s => s.FindAll()).Returns(new List<User>());

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<User, GetUser>(It.IsAny<User>())).Returns(new GetUser());

            var controller = new UsersController(repo.Object, mapper.Object, null);

            // Act
            var response = controller.Get();

            // Assert
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// Posting the user returns HTTP created status.
        /// </summary>
        [TestMethod]
        public void Post_User_Returns_Http_Created_Status()
        {
            // Arrange
            var repo = new Mock<IUserRepository>();
            repo.Setup(s => s.Add(It.IsAny<User>()));

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<User, GetUser>(It.IsAny<User>())).Returns(new GetUser());

            var encryptionService = new Mock<IEncryptionService>();
            encryptionService.Setup(e => e.Encrypt(It.IsAny<string>())).Returns(new byte[10]);

            var controller = new UsersController(repo.Object, mapper.Object, encryptionService.Object);
            Common.RegisterContext(controller, "users");

            var user = new PostUser()
            {
                Username = "admin",
                Password = "admin"
            };

            // Act
            HttpResponseMessage response = controller.Post(user);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        private User GetUserResponse()
        {
            return new User()
            {
                ID = Guid.NewGuid(),
                Username = "admin"
            };
        }
    }
}
