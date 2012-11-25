using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Entrants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Model.Tests.Entrants
{
    /// <summary>
    /// Tests for the entrant class.
    /// </summary>
    [TestClass]
    public class EntrantTests
    {
        /// <summary>
        /// Entrants the is valid.
        /// </summary>
        [TestMethod]
        public void EntrantIsValid()
        {
            // Arrange
            MockContact mockContact = new MockContact();
            mockContact.SetValidState(true);

            Entrant entrant = new Entrant();
            entrant.Answer = CompetitionAnswer.A;
            entrant.CompetitionKey = "WINPRIZE";
            entrant.Source = EntrantSource.Sms;
            entrant.Contact = mockContact;

            // Act
            bool isValid = entrant.IsValid;

            // Assert
            Assert.IsTrue(isValid);            
        }

        /// <summary>
        /// Entrants the is not valid.
        /// </summary>
        [TestMethod]
        public void EntrantIsNotValid()
        {
            // Arrange
            Entrant entrant = new Entrant();
            entrant.Contact = new MockContact();

            // Act
            bool isValid = entrant.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Entrants the with contact validation errors is not valid.
        /// </summary>
        [TestMethod]
        public void EntrantWithContactValidationErrorsIsNotValid()
        {
            // Arrange
            MockContact mockContact = new MockContact();

            Entrant entrant = new Entrant();
            entrant.Answer = CompetitionAnswer.A;
            entrant.CompetitionKey = "WINPRIZE";
            entrant.Source = EntrantSource.Sms;
            entrant.Contact = mockContact;
            
            // Act
            bool isValid = entrant.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        public class MockContact : IEntrantContact
        {

            public string Contact
            {
                get
                {
                    throw new System.NotImplementedException();
                }
                set
                {
                    throw new System.NotImplementedException();
                }
            }

            public EntrantContactType ContactType
            {
                get { throw new System.NotImplementedException(); }
            }

            /// <summary>
            /// Gets the validation errors.
            /// </summary>
            /// <value>
            /// The validation errors.
            /// </value>
            public ValidationErrors ValidationErrors
            {
                get
                {
                    if (IsValid)
                    {
                        return new ValidationErrors();
                    }
                    else
                    {
                        // add one row to put this in an invalid state
                        ValidationErrors errors = new ValidationErrors();
                        errors.Add(new ValidationError("property", "required"));
                        return errors;
                    }
                }
            }

            /// <summary>
            /// Gets a value indicating whether this instance is valid.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
            /// </value>
            public bool IsValid { get; private set; }

            /// <summary>
            /// Sets the state of the valid.
            /// </summary>
            /// <param name="isValid">if set to <c>true</c> [is valid].</param>
            public void SetValidState(bool isValid)
            {
                this.IsValid = isValid;
            }
        }
    }
}
