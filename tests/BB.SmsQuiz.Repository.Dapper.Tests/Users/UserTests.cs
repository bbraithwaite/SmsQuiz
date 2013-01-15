using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BB.SmsQuiz.Model.Users;
using BB.SmsQuiz.Repository.Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BB.SmsQuiz.Infrastructure.Encryption;

namespace BB.SmsQuiz.Repository.Tests.Users
{
    [TestClass]
    public class UserTests
    {
        private IUserRepository _repository;

        private string USERNAME;
        private byte[] PASSWORD;

        [TestInitialize]
        public void SetUp()
        {
            _repository = new UserRepository();
            PASSWORD = Encoding.UTF8.GetBytes(StringExtensions.GetRandomString(10));
            USERNAME = StringExtensions.GetRandomString(10);
        }

        [TestMethod]
        public void UserCrud()
        {
            Guid newID = Create();
            GetByID(newID);
            GetByUsername();
            GetAll();
            Update(newID);
            Delete(newID);
        }

        private Guid Create()
        {
            // Arrange
            User user = new User();
            user.Username = USERNAME;
            user.Password = new EncryptedString(PASSWORD); 

            // Act
            _repository.Add(user);

            // Assert
            Assert.AreNotEqual(Guid.Empty, user.ID, "Creating new record does not return id");

            return user.ID;
        }

        private void GetByUsername()
        {
            // Act
            User user = _repository.Find(new { Username = USERNAME }).SingleOrDefault();

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(USERNAME, user.Username);
            Assert.IsTrue(user.Password.EncryptedValue.Count() > 0);
        }

        private void Update(Guid id)
        {
            // Arrange
            User user = _repository.FindByID(id);
            user.Username = USERNAME + "updated";

            // Act
            _repository.Update(user);
            User updatedRecord = _repository.FindByID(id);

            // Assert
            Assert.AreEqual(USERNAME + "updated", updatedRecord.Username, "Record is not updated.");
        }

        private void GetAll()
        {
            // Act
            IEnumerable<User> items = _repository.FindAll();

            // Assert
            Assert.IsTrue(items.Count() > 0, "GetAll returned no items.");
        }

        private void GetByID(Guid id)
        {
            // Act
            User user = _repository.FindByID(id);

            // Assert
            Assert.AreEqual(id, user.ID);
            Assert.AreEqual(USERNAME, user.Username);
            Assert.IsTrue(user.Password.EncryptedValue.Count() > 0);
        }

        private void Delete(Guid id)
        {
            // Arrange
            User user = _repository.FindByID(id);

            // Act
            _repository.Remove(user);
            user = _repository.FindByID(id);

            // Assert
            Assert.IsNull(user, "Record is not deleted.");
        }
    }
}
