// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDataMapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Linq;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Model.Users;
using Dapper;

namespace BB.SmsQuiz.DataMappers
{
    /// <summary>
    /// The user mapper.
    /// </summary>
    public class UserDataMapper : AbstractDataMapper<User>, IUserDataMapper
    {
        /// <summary>
        /// Gets the table name.
        /// </summary>
        protected override string TableName
        {
            get { return "Users"; }
        }

        /// <summary>
        /// Find by username.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public User FindByUsername(string username)
        {
            return FindSingle("SELECT * FROM Users WHERE Username=@Username", new { Username = username });
        }

        /// <summary>
        /// Find by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public User FindById(Guid id)
        {
            return FindSingle("SELECT * FROM Users WHERE ID=@ID", new { ID = id });
        }

        /// <summary>
        /// Find user by authentication token.
        /// </summary>
        /// <param name="authenticationToken">The authentication token</param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public User FindByAuthToken(string authenticationToken)
        {
            // TODO: Add real implementation. This is a stub
            return FindSingle("SELECT TOP 1 * FROM Users", new { AuthToken = authenticationToken });
        }

        /// <summary>
        /// Insert user.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Insert(User item)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                item.ID =
                    cn.Query<Guid>(
                        "INSERT INTO Users (Username, Password) OUTPUT inserted.ID VALUES (@Username, @Password)", 
                        new { item.Username, Password = item.Password.EncryptedValue }).First();
            }
        }

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Update(User item)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(
                    "UPDATE Users SET UserName=@UserName, Password=@Password WHERE ID=@ID", 
                    new { item.ID, item.Username, Password = item.Password.EncryptedValue });
            }
        }

        /// <summary>
        /// Maps from the data to the User entity
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public override User Map(dynamic result)
        {
            var item = new User
            {
                ID = result.ID,
                Username = result.Username,
                Password = new EncryptedString(result.Password)
            };

            return item;
        }
    }
}