using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Model.Users;
using Dapper;

namespace BB.SmsQuiz.Repository.Dapper
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        public UserRepository() : base("Users") { }

        /// <summary>
        /// Mappings the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A mapping object of sql parameters to values.</returns>
        internal override dynamic Mapping(User item)
        {
            return new
            {
                ID = item.ID,
                Username = item.Username,
                Password = item.Password.EncryptedValue
            };
        }

        /// <summary>
        /// Finds the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A user by ID.</returns>
        public override User FindByID(Guid id)
        {
            User item = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var result = cn.Query("SELECT * FROM Users WHERE ID=@ID", new { ID = id }).SingleOrDefault();

                if (result != null)
                {
                    item = MapUser(result);
                }
            }

            return item;
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public override IEnumerable<User> FindAll()
        {
            var items = new List<User>();

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var results = cn.Query("SELECT * FROM Users");

                for (int i = 0; i < results.Count(); i++)
                {
                    items.Add(MapUser(results.ElementAt(i)));
                }
            }

            return items;
        }

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The param.</param>
        /// <returns>
        /// A list of users that match the query.
        /// </returns>
        public override IEnumerable<User> Find(string query, dynamic param)
        {
            var items = new List<User>();

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var results = cn.Query("SELECT * FROM Users WHERE " + query, (object)param);

                for (int i = 0; i < results.Count(); i++)
                {
                    items.Add(MapUser(results.ElementAt(i)));
                }
            }

            return items; 
        }

        /// <summary>
        /// Maps the user.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>A user entity from the dynamic result.</returns>
        private static User MapUser(dynamic result)
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
