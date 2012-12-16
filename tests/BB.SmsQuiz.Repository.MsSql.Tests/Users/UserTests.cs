using System;
using System.Collections.Generic;
using System.Linq;
using BB.SmsQuiz.Model.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BB.SmsQuiz.Infrastructure.Encryption;

namespace BB.SmsQuiz.Repository.Tests.Users
{
    [TestClass]
    public class UserTests
    {
        private IUserRepository _repository;

        /// <summary>
        /// Sets up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _repository = (IUserRepository)RepositoryFactory.Instance("UserRepository");
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

        /// <summary>
        /// Creates this instance.
        /// </summary>
        private Guid Create()
        {
            // Arrange
            User user = new User();
            user.Username = "tester";
            user.Password = new EncryptedString("password"); 

            // Act
            _repository.Add(user);

            // Assert
            Assert.AreNotEqual(Guid.Empty, user.ID, "Creating new record does not return id");

            return user.ID;
        }

        /// <summary>
        /// Gets the by username.
        /// </summary>
        private void GetByUsername()
        {
            // Act
            User user = _repository.Find(u => u.Username == "tester").SingleOrDefault();

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual("tester", user.Username);
            Assert.IsFalse(String.IsNullOrEmpty(user.Password.EncryptedValue));
        }

        /// <summary>
        /// Updates the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        private void Update(Guid id)
        {
            // Arrange
            User user = _repository.FindByID(id);
            user.Username = "testerUpdated";

            // Act
            _repository.Update(user);
            User updatedRecord = _repository.FindByID(id);

            // Assert
            Assert.AreEqual("testerUpdated", updatedRecord.Username, "Record is not updated.");
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        private void GetAll()
        {
            // Act
            IEnumerable<User> items = _repository.FindAll();

            // Assert
            Assert.IsTrue(items.Count() > 0, "GetAll returned no items.");
        }

        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="id">The id of the competition.</param>
        private void GetByID(Guid id)
        {
            // Act
            User user = _repository.FindByID(id);

            // Assert
            Assert.AreEqual(id, user.ID);
            Assert.AreEqual("tester", user.Username);
            Assert.IsFalse(String.IsNullOrEmpty(user.Password.EncryptedValue));
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
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
