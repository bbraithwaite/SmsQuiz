// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractDataMapper.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace BB.SmsQuiz.DataMappers
{
    /// <summary>
    /// The abstract data mapper.
    /// </summary>
    /// <typeparam name="T">
    /// The data entity.
    /// </typeparam>
    public abstract class AbstractDataMapper<T>
    {
        /// <summary>
        /// Gets the table name.
        /// </summary>
        protected abstract string TableName { get; }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        protected IDbConnection Connection
        {
            get
            {
                return
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["BBSmsQuizDatabaseContext"].ConnectionString);
            }
        }

        /// <summary>
        /// Find a single record.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="param">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T FindSingle(string query, dynamic param)
        {
            dynamic item = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var result = cn.Query(query, (object) param).SingleOrDefault();

                if (result != null)
                {
                    item = Map(result);
                }
            }

            return item;
        }

        /// <summary>
        /// Find all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public virtual IEnumerable<T> FindAll()
        {
            var items = new List<T>();

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var results = cn.Query("SELECT * FROM " + TableName).ToList();

                for (int i = 0; i < results.Count(); i++)
                {
                    items.Add(Map(results.ElementAt(i)));
                }
            }

            return items;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public virtual void Delete(Guid id)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM " + TableName + " WHERE ID=@ID", new { ID = id });
            }
        }

        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="dynamic"/>.
        /// </returns>
        public abstract T Map(dynamic result);
    }
}