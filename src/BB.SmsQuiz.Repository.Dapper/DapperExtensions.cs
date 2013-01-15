using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BB.SmsQuiz.Repository.Dapper
{
    /// <summary>
    /// Extension methods for the Dapper.net plugin.
    /// </summary>
    public static class DapperExtensions
    {
        /// <summary>
        /// Inserts the specified connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">The connection.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public static T Insert<T>(this IDbConnection connection, string tableName, dynamic param)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(connection, DynamicQuery.GetInsertQuery(tableName, param), param);
            return result.First();
        }

        /// <summary>
        /// Updates the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="param">The param.</param>
        public static void Update(this IDbConnection connection, string tableName, dynamic param)
        {
            SqlMapper.Execute(connection, DynamicQuery.GetUpdateQuery(tableName, param), param);
        }
    }
}